using Godot;
using System;

//Inherits State
public partial class BasicEnemyHurt : State
{
    //Variable for gravity
    public float gravity = 980;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        if (StateAnimation.IsPlaying())
        {
            StateAnimation.Stop();
        }
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

        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Function to check when the attack animation has finished
    /// </summary>
    /// <param name="anim_name"></param>
    public void OnAnimationPlayerFinished(string anim_name)
    {
        //Go to chase
        EmitSignal(signal: "StateTransition", this, "BasicEnemyChase");
    }
}
