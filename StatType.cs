using Godot;
using System;

public partial class StatType : Button
{
    //Export variable for label to display information
    [Export] public Label StatInformation = new();
    [Export] public Label StatCount = new();

    //Signal for stats to emit towards the stat script
    [Signal] public delegate void ChangeStatEventHandler(StatType statTarget, float statChange);

    //Variable for global player variables
    public PlayerGlobals PlayerGlobalsVariable = new();
}
