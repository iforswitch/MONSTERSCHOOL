using Godot;
using System;

public partial class stat : Control
{
	//Signal to indicate the stat is done
	[Signal] public delegate void StatFinishedEventHandler();

    //Variable for global player variables
    PlayerGlobals PlayerGlobalsVariable = new();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //Set global player variable
		PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");

        //Connect each stat child to the signal function
		foreach (StatType child in GetTree().GetNodesInGroup("Stats"))
		{
            child.ChangeStat += OnChangeStat;
        }
	}

    /// <summary>
    /// Main signal function to change the global player variables
    /// </summary>
    /// <param name="statTarget"></param>
    /// <param name="statChange"></param>
	public void OnChangeStat(StatType statTarget, float statChange)
	{
        switch (statTarget.Name)
        {
            case "AttackStat":
                //Multiply the damage by the percentage, increase
                PlayerGlobalsVariable.Damage *= (statChange + 1);
                PlayerGlobalsVariable.AttackStat += 1;
                break;

            case "DexterityStat":
                //Multiply the cooldown by the percentage, decrease
                PlayerGlobalsVariable.Cooldown *= (1-statChange);
                PlayerGlobalsVariable.DexterityStat += 1;
                break;

            case "HealthStat":
                //Multiply the max health by the percentage, increase
                PlayerGlobalsVariable.MaxHealth *= (statChange + 1);
                PlayerGlobalsVariable.CurrentHealth *= (statChange + 1);
                PlayerGlobalsVariable.HealthStat += 1;
                break;
        }

        //Emit signal to the PlayerFSM
        EmitSignal("StatFinished");

        //Destroy self
		QueueFree();
	}
}
