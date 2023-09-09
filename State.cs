using Godot;
using System;

public partial class State : Node
{
    //Export variable for the subject body
	[Export] public CharacterBody2D SubjectBody = new();

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
