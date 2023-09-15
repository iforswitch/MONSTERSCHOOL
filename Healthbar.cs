using Godot;
using System;

public partial class Healthbar : TextureProgressBar
{
    //Export variable for label
    [Export] public Label HealthInfo = new();

    //Player global variable
    public PlayerGlobals PlayerGlobalsVariable = new();

    public override void _Ready()
    {
        //Set global player variable
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");

        //Set label text
        HealthInfo.Text = $"{Value}/{PlayerGlobalsVariable.MaxHealth}";

        //Set Max to the max health;
        MaxValue = PlayerGlobalsVariable.MaxHealth;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        //Set Max to the max health;
        MaxValue = PlayerGlobalsVariable.MaxHealth;

        //Set the value to the current health
        Value = PlayerGlobalsVariable.CurrentHealth;

        //Set label text
        HealthInfo.Text = $"{Math.Round(Value)}/{Math.Round(PlayerGlobalsVariable.MaxHealth)}";
    }
}
