using Godot;
using System;

//Inherits State class
public partial class PlayerFall : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

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
