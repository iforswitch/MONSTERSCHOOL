using Godot;
using System;

public partial class FSM : Node
{
	//Export variable for the state the FSM will start with
	[Export] public State InitialState = new State();
}

