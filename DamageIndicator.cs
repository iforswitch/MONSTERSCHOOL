using Godot;
using System;

public partial class DamageIndicator : Node
{
	//Signal for handling the information of the indicators
	[Signal] public delegate void DamageIndicateEventHandler(CharacterBody2D damagedBody, float damageValue);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every physics frame. 'delta' is the elapsed time since the previous physics frame.
	public override void _PhysicsProcess(double delta)
	{
	}
}
