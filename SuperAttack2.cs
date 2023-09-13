using Godot;
using System;

public partial class SuperAttack2 : State
{
    //Export variable for rolling speed
    [Export] public float RollSpeed;

    //Export variable for effects
    [Export] public Node2D EffectNodes = new();

    //Export variable for damage
    [Export] public float damage;

    //Variable for movement direction
    public Vector2 Direction = new();

    //variable for gravity
    public float gravity = 980;

    //Local timer variable
    public Timer SuperAttack2Cooldown = new();

    //Variable for checking to hit is valid
    public bool CanHit;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);
        SuperAttack2Cooldown = RollCooldown;

        //Set global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        damage = PlayerGlobalsVariable.Damage;

        CanHit = true;
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
        GD.Print($"{Name} exited.");

        //Cooldown start
        SuperAttack2Cooldown.Start();
    }

    /// <summary>
    /// Function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = SubjectBody.Velocity;
        velocity.Y += gravity * (float)delta;

        //Sprite direction check
        if (PivotNode.Scale.X == -1)
        {
            Direction = Vector2.Left;
            StateSprite.FlipH = true;
        }

        if (PivotNode.Scale.X == 1)
        {
            Direction = Vector2.Right;
            StateSprite.FlipH = false;
        }

        //Roll a distance
        if (Direction.X == 0)
        {
            velocity.X = RollSpeed * PivotNode.Scale.X;
        }
        else
        {
            velocity.X = RollSpeed * Direction.X;
        }

        if (Direction.X != 0)
        {
            PivotNode.Scale = new Vector2(Direction.X, 1);
        }

        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Function to check when the attack animation has finished
    /// </summary>
    /// <param name="anim_name"></param>
    public void OnAnimationPlayerFinished(string anim_name)
    {
        //Go to idle
        if (anim_name == Name)
        {
            EmitSignal(signal: "StateTransition", this, "PlayerIdle");
        }
    }

    /// <summary>
    /// Function for overlapping bodies in attack hitbox
    /// </summary>
    public void OnOverlappingBodiesCheck(Godot.Collections.Array<Node2D> bodyList)
    {
        if (bodyList != null && CanHit)
        {
            CanHit = false;
            foreach (var body in bodyList)
            {
                for (int i = 0; i < body.GetChildCount(); i++)
                {
                    if (body.GetChild(i) is FSM)
                    {
                        FSM enemyFSM = body.GetChild(i) as FSM;
                        enemyFSM.Hit(damage);
                    }
                }
            }
        }
    }
}
