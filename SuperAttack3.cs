using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SuperAttack3 : State
{
    //Export timer variable for the cooldown
    [Export] public Timer SuperAttack3CD = new();

    //Local timer variable
    public Timer SuperAttack3Timer = new();

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
        SuperAttack3Timer = RollCooldown;
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        CharacterBody2D parent = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        Node2D instanceEffect = (Node2D)SuperAttack3Effect.Instantiate();
        parent.AddChild(instanceEffect);
        instanceEffect.GlobalPosition = parent.GlobalPosition;
        PlayerGlobalsVariable.IsSpecialAttack3 *= -1;

        if (PlayerGlobalsVariable.IsSpecialAttack3 == 1)
        {
            StateAnimation.Play(Name);
        }
        else
        {
            StateAnimation.Stop();
        }

        PlayerGlobalsVariable.EmitSignal("SpecialAttack3");
        entered = true;
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
        GD.Print($"{Name} exited.");

        //Conditions to turn off / on the ability
        if (PlayerGlobalsVariable.IsSpecialAttack3 == 1)
        {
            SuperAttack3Timer.Start();
        }
        else
        {
            SuperAttack3Timer.Stop();
        }

        SuperAttack3CD.Start();
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
