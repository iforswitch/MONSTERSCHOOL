using Godot;
using System;

public partial class BasicAttackbar : TextureProgressBar
{
    //Timer variable for value
    public Timer t0 = new();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set timer
        t0 = (Timer)GetTree().GetFirstNodeInGroup("BasicAttackTimer");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        //Set max to cooldown
        MaxValue = t0.WaitTime;

        //Set step
        Step = t0.WaitTime / 360;

        //Set value to the current cooldown time
        Value = t0.TimeLeft;
    }
}
