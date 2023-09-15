using Godot;
using System;
using System.ComponentModel;

public partial class AttackStat : StatType
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //Set global player variable
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");

        //Set label to information
        StatCount.Text = $"x{PlayerGlobalsVariable.AttackStat}";
        StatInformation.Text = $"+{Math.Round(CalculateStatChange(PlayerGlobalsVariable.AttackStat), 2)}% to all damage.";
    }

    /// <summary>
    /// Function that executes for every press of the button
    /// </summary>
    public override void _Pressed()
    {
        //Emit signal to main stat script
        EmitSignal("ChangeStat", this, CalculateStatChange(PlayerGlobalsVariable.AttackStat));
    } 

    /// <summary>
    /// Function to calculate the corresponding upgrade degradation
    /// </summary>
    /// <param name="CurrentStat"></param>
    /// <returns></returns>
    public float CalculateStatChange(float CurrentStat)
    {
        float increase = 0.15f * (0.15f / (CurrentStat/100 + 0.15f));
        return increase;
    }
}
