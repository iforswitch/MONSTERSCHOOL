using Godot;
using System;

//Inherits State class
public partial class BasicEnemyIdle : State
{
    //Export variable for the wander timer
    [Export] public Timer WanderTimerIdle = new();

    //Variable for randomness
    public RandomNumberGenerator r = new();

    //Variable for gravity
    public float gravity = 980;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        RandomiseWanderTimer();
        WanderTimerIdle.Start();
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

        if (!SubjectBody.IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }
        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Randomise the duration of wander timer
    /// </summary>
    public void RandomiseWanderTimer()
    {
        if (WanderTimerIdle.TimeLeft == 0)
        {
            WanderTimerIdle.WaitTime = r.RandfRange(0.5f, 1.5f);
        }
    }

    /// <summary>
    /// Timeoutsignal for the wander timer
    /// </summary>
    public void OnWanderTimerIdleTimeout()
    {
        EmitSignal(signal: "StateTransition", this, "BasicEnemyRun");
    }

    /// <summary>
    /// Chase the player if too close
    /// </summary>
    public void OnDetectionRadiusEntered(Node2D player)
    {
        EmitSignal(signal: "StateTransition", this, "BasicEnemyChase");
    }
}
