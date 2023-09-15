using Godot;
using System;

public partial class Score : Label
{
	//Player global variables
	public PlayerGlobals PlayerGlobalsVariable = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Set global player variables
		PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Text = $"Skeletons Killed: {PlayerGlobalsVariable.Score}";
	}
}
