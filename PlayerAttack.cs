using Godot;
using System;

public partial class PlayerAttack : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for damage
    [Export] public float damage;

    //Export variable attack buffering
    [Export] public Timer AttackBuffer = new();

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
    /// Function for the key input of the state
    /// </summary>
    /// <param name="event"></param>
    public override void UnhandledKeyInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Attack") && AttackBuffer.TimeLeft > 0)
        {
            StateAnimation.Play($"{Name}_2");
            AttackBuffer.Stop();
            CanHit = true;
        }
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
    /// Detects enemies or objects to attack
    /// </summary>
    /// <param name="enemy"></param>
    public void OnHitboxEntered(Node2D enemy)
    {
        if (enemy != null && CanHit)
        {
            CanHit = false;
            for (int i = 0; i < enemy.GetChildCount(); i++)
            {
                if (enemy.GetChild(i) is FSM)
                {
                    FSM enemyFSM = enemy.GetChild(i) as FSM;
                    enemyFSM.Hit(damage);
                }
            }
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
}
