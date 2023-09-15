using Godot;
using System;

public partial class damage_indicator_label : Label
{
	//Export variable for timer
	[Export] Timer timer = new();

	/// <summary>
	/// Function to execute once node enters the scene
	/// </summary>
    public override void _Ready()
    {
		timer.Start();
    }

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

	/// <summary>
	/// Function to queue free
	/// </summary>
	public void OnTimerTimeout()
	{
		QueueFree();
	}
}
