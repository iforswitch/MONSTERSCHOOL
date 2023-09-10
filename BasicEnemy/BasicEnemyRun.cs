using Godot;
using System;
using System.Collections.Generic;

//Inherits State class
public partial class BasicEnemyRun : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for the wander timer
    [Export] public Timer WanderTimerRun = new();

    //Export variable for raycast
    [Export] public Node2D RaycastGroup = new();

    //Variable for movement direction
    public Vector2 Direction = new();

    //Variable for randomness
    public RandomNumberGenerator r = new();

    //variable for gravity
    public float gravity = 980;

    //Variable for direction change
    public bool ChangeDirectionCheck;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        RandomiseWanderTimer();
        WanderTimerRun.Start();
        Direction = RandomiseDirection();
        ChangeDirectionCheck = true;
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
        if (!SubjectBody.IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        foreach (RayCast2D raycast in RaycastGroup.GetChildren())
        {
            if (raycast.IsColliding() && ChangeDirectionCheck)
            {
                ChangeDirectionCheck = false;
                ChangeDirection();
            }
            else if (!raycast.IsColliding())
            {
                ChangeDirectionCheck = true;
            }
        }

        if (Direction == Vector2.Left)
        {
            StateSprite.FlipH = true;
        }
        else if (Direction == Vector2.Right)
        {
            StateSprite.FlipH = false;
        }

        RaycastGroup.Scale = new Vector2(Direction.X, 1);
        velocity.X = Direction.X * MovementSpeed;

        SubjectBody.Velocity = velocity;
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
        return randomDirection;
    }

    /// <summary>
    /// Chase the player if too close
    /// </summary>
    public void OnDetectionRadiusEntered(Node2D player)
    {
        EmitSignal(signal: "StateTransition", this, "BasicEnemyChase");
    }

    /// <summary>
    /// If there is a wall, change the direction
    /// </summary>
    /// <returns></returns>
    public Vector2 ChangeDirection()
    {
        Direction.X = -Direction.X;
        return Direction;
    }
}
