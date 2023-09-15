using Godot;
using System;

public partial class SpecialAttack1Bar : TextureProgressBar
{
    //Timer variable for value
    public Timer t1 = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set timer
        t1 = (Timer)GetTree().GetFirstNodeInGroup("SpecialAttack1Cooldown");

        //Set max to cooldown
        MaxValue = t1.WaitTime;

        //Set step
        Step = t1.WaitTime / 360;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        //Set value to the current cooldown time
        Value = t1.TimeLeft;
    }
}
