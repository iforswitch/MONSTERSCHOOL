using Godot;
using System;


//Inherits State class
public partial class PlayerRun : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Variable for movement direction
    public Vector2 Direction = new();

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
        if (Input.IsActionPressed("MoveLeft"))
        {
            Direction = Vector2.Left;
            StateSprite.FlipH = true;
        }

        if (Input.IsActionPressed("MoveRight"))
        {
            Direction = Vector2.Right;
            StateSprite.FlipH = false;
        }

        //Go to PlayerJump if SubjectBody is jumping
        if (Input.IsActionPressed("MoveJump") && SubjectBody.IsOnFloor())
        {
            EmitSignal(signal: "StateTransition", this, "PlayerJump");
        }

        //Go to PlayerFall if not grounded
        if (!SubjectBody.IsOnFloor())
        {
            EmitSignal(signal: "StateTransition", this, "PlayerFall");
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
        //Transition to PlayerIdle state if not moving
        if (@event.IsActionReleased("MoveLeft") || @event.IsActionReleased("MoveRight"))
        {
            EmitSignal(signal: "StateTransition", this, "PlayerIdle");
        }
    }
}
