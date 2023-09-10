using Godot;
using System;

public partial class player : CharacterBody2D
{
	/// <summary>
	/// This function handles the physic components on physics time, rather than frame time
	/// </summary>
	/// <param name="delta"></param>
	public override void _PhysicsProcess(double delta)
	{
		//Calling moveandslide to move the player
		MoveAndSlide();
	}
}
