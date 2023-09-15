using Godot;
using System;

public partial class TextureProgressBar : Godot.TextureProgressBar
{
	//Timer variable for value
	public Timer t = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Set timer
		t = (Timer)GetTree().GetFirstNodeInGroup("RollCooldown");

		//Set max to cooldown
		MaxValue = t.WaitTime;

		//Set step
		Step = t.WaitTime / 360;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		//Set value to the current cooldown time
		Value = t.TimeLeft;
	}
}
