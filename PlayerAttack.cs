using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAttack : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for damage
    public float damage;

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

        //Set global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        damage = PlayerGlobalsVariable.Damage;
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

        if (Input.IsActionPressed("Attack") && AttackBuffer.TimeLeft > 0)
        {
            StateAnimation.Play($"{Name}_2");
            AttackBuffer.Stop();
            CanHit = true;
        }

        GD.Print($"attack {PlayerGlobalsVariable.Damage}");

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
            if (AttackBuffer.TimeLeft == 0)
            {
                AttackBuffer.Start();
            }
        }
        else if (anim_name == $"{Name}_2")
        {
            ExitAttackState();
        }
    }

    /// <summary>
    /// Function to check the attack buffer for the second attack
    /// </summary>
    public void OnAttackBufferTimeout()
    {
        ExitAttackState();
    }

    /// <summary>
    /// General function to exit the attack state
    /// </summary>
    public void ExitAttackState()
    {
        //Go to PlayerFall if not grounded
        if (!SubjectBody.IsOnFloor())
        {
            EmitSignal(signal: "StateTransition", this, "PlayerFall");
        }
        else
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
