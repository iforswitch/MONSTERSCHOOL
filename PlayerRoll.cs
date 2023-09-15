using Godot;
using System;

public partial class PlayerRoll : State
{
    //Export variable for rolling speed
    [Export] public float RollSpeed;

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
        StateAnimation.Play(Name);

        //Set the global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        RollSpeed = PlayerGlobalsVariable.RollSpeed;

        //Set timer wait time to 2 seconds with CD reduction
        RollCooldown.WaitTime = 2 * PlayerGlobalsVariable.Cooldown;
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
        GD.Print($"{Name} exited.");

        //Cooldown start
        RollCooldown.Start();
    }

    /// <summary>
    /// Function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = SubjectBody.Velocity;
        velocity.Y += gravity * (float)delta;

        //Sprite direction check
        if (PivotNode.Scale.X == -1)
        {
            Direction = Vector2.Left;
            StateSprite.FlipH = true;
        }

        if (PivotNode.Scale.X == 1)
        {
            Direction = Vector2.Right;
            StateSprite.FlipH = false;
        }

        //Roll a distance
        if (Direction.X == 0)
        {
            velocity.X = RollSpeed * PivotNode.Scale.X;
        }
        else
        {
            velocity.X = RollSpeed * Direction.X;
        }

        if (Direction.X != 0)
        {
            PivotNode.Scale = new Vector2(Direction.X, 1);
        }

        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Function to check when the attack animation has finished
    /// </summary>
    /// <param name="anim_name"></param>
    public void OnAnimationPlayerFinished(string anim_name)
    {
        //Go to idle
        if (anim_name == Name)
        {
            EmitSignal(signal: "StateTransition", this, "PlayerIdle");
        }
    }
}
