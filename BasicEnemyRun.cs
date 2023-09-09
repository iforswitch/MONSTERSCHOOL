using Godot;
using System;
using System.Collections.Generic;

//Inherits State class
public partial class BasicEnemyRun : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Variable for movement direction
    public Vector2 Direction = new();

    //Export variable for the wander timer
    [Export] public Timer WanderTimerRun = new();

    //Variable for randomness
    public RandomNumberGenerator r = new();

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        RandomiseWanderTimer();
        WanderTimerRun.Start();
        Direction = RandomiseDirection();
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

        velocity.X = Direction.X * MovementSpeed;

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
    /// Randomise the duration of wander timer
    /// </summary>
    public void RandomiseWanderTimer()
    {
        if (WanderTimerRun.TimeLeft == 0)
        {
            WanderTimerRun.WaitTime = r.RandfRange(2f, 3f);
        }
    }

    /// <summary>
    /// Timeoutsignal for the wander timer
    /// </summary>
    public void OnWanderTimerRunTimeout()
    {
        EmitSignal(signal: "StateTransition", this, "BasicEnemyIdle");
    }

    /// <summary>
    /// Randomse the direction of the movement
    /// </summary>
    /// <returns></returns>
    public Vector2 RandomiseDirection()
    {
        Vector2 randomDirection = new();
        int[] randomX = {-1, 1};
        randomDirection = new Vector2(randomX[GD.Randi() % randomX.Length], 0);
        GD.Print(randomDirection.X);
        return randomDirection;
    }
}
