using Godot;
using System;

public partial class FSM : Node
{
	//Export variable for the state the FSM will start with
	[Export] public State InitialState = new State();

    //Export variable for max health
    [Export] public float MaxHealth;

    /// <summary>
    /// Virtual function for damaging the subject body
    /// </summary>
    /// <param name="damage"></param>
    public virtual void Hit(float damage)
    {

    }
}

