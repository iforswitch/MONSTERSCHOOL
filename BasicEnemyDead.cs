using Godot;
using System;

//Inherits State
public partial class BasicEnemyDead : State
{
    //Export variable for death timer
    [Export] public Timer DeathTimer = new();

    //Variable for gravity
    public float gravity = 980;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);

        //Set global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
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
    /// Function to check when the attack animation has finished
    /// </summary>
    /// <param name="anim_name"></param>
    public void OnAnimationPlayerFinished(string anim_name)
    {
        if (anim_name == Name)
        {
            DeathTimer.Start();

            //Increase score
            PlayerGlobalsVariable.SkeletonsKilled += 1;
        }
    }

    /// <summary>
    /// Kill body after death timer timeout
    /// </summary>
    public void OnDeathTimerTimeout()
    {
        GetParent().GetParent().QueueFree();
    }
}
