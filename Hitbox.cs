using Godot;
using System;
using System.Collections.Generic;

public partial class Hitbox : Area2D
{
	//Signal for checking multiple bodies on collision
	[Signal] public delegate void OverlappingBodiesCheckEventHandler();

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Monitoring) 
		{
			if (HasOverlappingBodies())
			{
				EmitSignal("OverlappingBodiesCheck", GetOverlappingBodies());
			}
		}
	}
}
