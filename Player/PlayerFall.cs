using Godot;
using System;

//Inherits State class
public partial class PlayerFall : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for special attack cooldown timer
    [Export] public Timer SpecialAttackCooldown = new();

    [Export] public Timer SpecialAttack2Cooldown = new();

    //Variable for movement direction
    public Vector2 Direction = new();

    //variable for gravity
    public float gravity = 980;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);
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

        //Gravity
        velocity.Y += gravity * (float)delta;

        //Go to PlayerIdle when grounded
        if (SubjectBody.IsOnFloor())
        {
            EmitSignal(signal: "StateTransition", this, "PlayerIdle");
        }

        //Horizontal movement whilst in the air
        if (Input.IsActionPressed("MoveLeft"))
        {
            Direction = Vector2.Left;
            StateSprite.FlipH = true;
        }
        else if (Input.IsActionPressed("MoveRight"))
        {
            Direction = Vector2.Right;
            StateSprite.FlipH = false;
        }
        else 
        {
            Direction = Vector2.Zero;
        }

        //Go to PlayerAttack if SubjectBody is attacking
        if (Input.IsActionPressed("Attack"))
        {
            EmitSignal(signal: "StateTransition", this, "PlayerAttack");
        }

        //Go to PlayerRoll if SubjectBody is rolling
        if (Input.IsActionPressed("Roll") && RollCooldown.TimeLeft == 0)
        {
            EmitSignal(signal: "StateTransition", this, "PlayerRoll");
        }

        //Go to SuperAttack if SubjectBody is doing SuperAttack
        if (Input.IsActionPressed("Special1") && SpecialAttackCooldown.TimeLeft == 0)
        {
            EmitSignal(signal: "StateTransition", this, "SuperAttack");
        }

        if (Input.IsActionPressed("Special2") && SpecialAttack2Cooldown.TimeLeft == 0)
        {
            EmitSignal(signal: "StateTransition", this, "SuperAttack2");
        }

        if (Direction.X != 0)
        {
            PivotNode.Scale = new Vector2(Direction.X, 1);
        }

        velocity.X = MovementSpeed * Direction.X;
        SubjectBody.Velocity = velocity;
    }
}
