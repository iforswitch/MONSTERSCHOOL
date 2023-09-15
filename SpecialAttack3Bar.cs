using Godot;
using System;

public partial class SpecialAttack3Bar : TextureProgressBar
{
    //Timer variable for value
    public Timer t3 = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set timer
        t3 = (Timer)GetTree().GetFirstNodeInGroup("SpecialAttack3Cooldown");

        //Set max to cooldown
        MaxValue = t3.WaitTime;

        //Set step
        Step = t3.WaitTime / 360;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        //Set value to the current cooldown time
        Value = t3.TimeLeft;
    }
}
