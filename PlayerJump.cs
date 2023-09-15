using Godot;
using System;

//Inherits State class
public partial class PlayerJump : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for player jump
    [Export] public float JumpStrength;

    //Export variable for special attack cooldown timer
    [Export] public Timer BasicAttack = new();
    [Export] public Timer SpecialAttackCooldown = new();
    [Export] public Timer SpecialAttack2Cooldown = new();
    [Export] public Timer SpecialAttack3Cooldown = new();

    //Variable for movement direction
    public Vector2 Direction = new();

    //Variable for checking falling
    public bool isfalling;

    //variable for gravity
    public float gravity = 980;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        isfalling = false;
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);

        //Set the global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        MovementSpeed = PlayerGlobalsVariable.Speed;
        JumpStrength = PlayerGlobalsVariable.JumpStrength;
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

        //Only jump if not falling
        if (Input.IsActionPressed("MoveJump") && !isfalling)
        {
            isfalling = true;
            velocity.Y = -JumpStrength;
        }

        if (isfalling)
        {
            velocity.Y += gravity * (float)delta;
        }

        //Go to PlayerFall when y position is increasing
        if (velocity.Y > 0)
        {
            EmitSignal(signal: "StateTransition", this, "PlayerFall");
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
        if (Input.IsActionPressed("Attack") && BasicAttack.TimeLeft == 0)
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

        if (Input.IsActionPressed("Special3") && SpecialAttack3Cooldown.TimeLeft == 0)
        {
            EmitSignal(signal: "StateTransition", this, "SuperAttack3");
        }

        if (Direction.X != 0)
        {
            PivotNode.Scale = new Vector2(Direction.X, 1);
        }

        velocity.X = MovementSpeed * Direction.X;
        SubjectBody.Velocity = velocity;
    }
}
