using Godot;
using System;

public partial class damage_indicator_label : Label
{
	/// <summary>
	/// Called every physics frame
	/// </summary>
	/// <param name="delta"></param>
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Position;

		//Move label up
		velocity.Y -= 5;

		Position = velocity;
	}
}
