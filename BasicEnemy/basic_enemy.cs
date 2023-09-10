using Godot;
using System;

public partial class basic_enemy : CharacterBody2D
{
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
}
