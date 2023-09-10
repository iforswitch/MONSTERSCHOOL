using Godot;
using System;

public partial class BasicEnemyAttack : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for damage
    [Export] public float damage;

    //Export variable for attack buffering
    [Export] public Timer AttackBuffer = new();

    //Export variable for area2D
    [Export] public Area2D AttackHitbox = new();

    //Variable for movement direction
    public Vector2 Direction = new();

    //Variable for gravity
    public float gravity = 980;

    //Variable for checking to hit is valid
    public bool CanHit;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);
        CanHit = true;
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
        GD.Print($"{Name} exited.");
    }

    /// <summary>
    /// Function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = SubjectBody.Velocity;
        velocity.X = 0;
        if (!SubjectBody.IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Function to check when the attack animation has finished
    /// </summary>
    /// <param name="anim_name"></param>
    public void OnAnimationPlayerFinished(string anim_name)
    {
        if (anim_name == Name)
        {
            EmitSignal(signal: "StateTransition", this, "BasicEnemyChase");
        }
    }

    /// <summary>
    /// Function to check the attack buffer for the second attack
    /// </summary>
    public void OnAttackBufferTimeout()
    {
        //ExitAttackState();
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
                        FSM playerFSM = body.GetChild(i) as FSM;
                        playerFSM.Hit(damage);
                        GD.Print("player hit");
                    }
                }
            }
        }
    }
}
