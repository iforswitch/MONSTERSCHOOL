using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SuperAttack3 : State
{
    //Local timer variable
    public Timer SuperAttack3Cooldown = new();

    //Variable for state enter check
    public bool entered;

    //Variable for super attack
    PackedScene SuperAttack3Effect = GD.Load<PackedScene>("res://super_attack_3_effect.tscn");

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);
        SuperAttack3Cooldown = RollCooldown;
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        PlayerGlobalsVariable.EmitSignal("SpecialAttack3");
        CharacterBody2D parent = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        Node2D instanceEffect = (Node2D)SuperAttack3Effect.Instantiate();
        parent.AddChild(instanceEffect);
        instanceEffect.GlobalPosition = parent.GlobalPosition;
        GD.Print(parent.GlobalPosition, instanceEffect.GlobalPosition, parent.Position, instanceEffect.Position);
        entered = true;
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
        GD.Print($"{Name} exited.");

        //Cooldown start
        SuperAttack3Cooldown.Start();
    }

    /// <summary>
    /// Function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void PhysicsProcess(double delta)
    {
        if (entered) 
        {
            entered = false;
            EmitSignal(signal: "StateTransition", this, "PlayerIdle");
        }
    }
}
