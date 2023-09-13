using Godot;
using System;

public partial class super_attack_3_effect : Node2D
{
    //Export Variable for animation player
    [Export] public AnimationPlayer AnimPlay = new();

    //Export variable for timer
    [Export] public Timer t = new();

    //Variable for global player variables
    PlayerGlobals PlayerGlobalsVariable = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimPlay.Play("SuperAttack3Effect");

        //Set global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
    }

    /// <summary>
    /// Called every physics frame
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        //If skill is not in use, destroy self
        if (PlayerGlobalsVariable.IsSpecialAttack3 == -1)
        {
            QueueFree();
        }
    }
}
