using Godot;
using System;

//Inherits State class
public partial class PlayerJump : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for player jump
    [Export] public float JumpStrength;

    //Variable for movement direction
    public Vector2 Direction = new();

    //Variable for checking falling
    public bool isfalling;

    //variable for gravity
    public float gravity = 980 * 20;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        isfalling = false;
        GD.Print($"{Name} entered.");
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
        }
        else if (Input.IsActionPressed("MoveRight"))
        {
            Direction = Vector2.Right;
        }
        else
        {
            Direction = Vector2.Zero;
        }

        velocity.X = MovementSpeed * Direction.X;
        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Function for the update of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void UpdateProcess(double delta)
    {

    }

    /// <summary>
    /// Function for the key input of the state
    /// </summary>
    /// <param name="event"></param>
    public override void UnhandledKeyInput(InputEvent @event)
    {

    }
}
