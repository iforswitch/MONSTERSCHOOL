using Godot;
using System;

//Inherits State class
public partial class PlayerIdle : State
{
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
        Vector2 velocity = new();
        if (velocity.X != 0)
        {
            velocity.X = 0;
        }

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
        //Transition to PlayerRun state if moving
        if (@event.IsActionPressed("MoveLeft") || @event.IsActionPressed("MoveRight"))
        {
            EmitSignal(signal: "StateTransition", this, "PlayerRun");
        }
    }
}
