using Godot;
using System;

public partial class State : Node
{
    //Export variable for the subject body
	[Export] public CharacterBody2D SubjectBody = new();

    //Export variable for state animations
    [Export] public AnimationPlayer StateAnimation = new();

    //Export variable for sprite
    [Export] public Sprite2D StateSprite = new();

    //Export variable for raycast
    [Export] public Node2D PivotNode = new();

    //Export variable for roll cooldown timer
    [Export] public Timer RollCooldown = new();

    //Signal for states to switch
    [Signal] public delegate void StateTransitionEventHandler(State emittingState, string targetState);

    /// <summary>
    /// Virtual function for entering the state
    /// </summary>
    public virtual void StateEnter()
    {

    }

    /// <summary>
    /// Virtual function for exiting the state
    /// </summary>
    public virtual void StateExit()
    {

    }

    /// <summary>
    /// Virtual function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public virtual void PhysicsProcess(double delta)
	{

	}

    /// <summary>
    /// Virtual function for the update of the state
    /// </summary>
    /// <param name="delta"></param>
    public virtual void UpdateProcess(double delta)
    {

    }

    /// <summary>
    /// Virtual function for the key input of the state
    /// </summary>
    /// <param name="event"></param>
    public virtual void UnhandledKeyInput(InputEvent @event)
    {

    }
}
