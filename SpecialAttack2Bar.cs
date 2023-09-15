using Godot;
using System;

public partial class SpecialAttack2Bar : TextureProgressBar
{
    //Timer variable for value
    public Timer t2 = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set timer
        t2 = (Timer)GetTree().GetFirstNodeInGroup("SpecialAttack2Cooldown");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        //Set max to cooldown
        MaxValue = t2.WaitTime;

        //Set step
        Step = t2.WaitTime / 360;

        //Set value to the current cooldown time
        Value = t2.TimeLeft;
    }
}
