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
        if (velocity.X != 0)
        {
            velocity.X = 0;
        }

        //Transition to PlayerRun state if moving
        if (Input.IsActionPressed("MoveLeft") || Input.IsActionPressed("MoveRight"))
        {
            EmitSignal(signal: "StateTransition", this, "PlayerRun");
        }

        //Go to PlayerFall if SubjectBody is not grounded
        if (!SubjectBody.IsOnFloor())
        {
            EmitSignal(signal: "StateTransition", this, "PlayerFall");
        }

        //Go to PlayerJump if SubjectBody is jumping
        if (Input.IsActionPressed("MoveJump") && SubjectBody.IsOnFloor())
        {
            EmitSignal(signal: "StateTransition", this, "PlayerJump");
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
        if (Input.IsActionPressed("Special1") && RollCooldown.TimeLeft == 0)
        {
            EmitSignal(signal: "StateTransition", this, "SuperAttack");
        }

        if (PivotNode.Scale.X == -1)
        {
            StateSprite.FlipH = true;
        }

        if (PivotNode.Scale.X == 1)
        {
            StateSprite.FlipH = false;
        }

        if (PivotNode.Scale.X != 0)
        {
            PivotNode.Scale = new Vector2(PivotNode.Scale.X, 1);
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
       
    }
}
