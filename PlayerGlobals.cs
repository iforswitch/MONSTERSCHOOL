using Godot;
using System;

public partial class PlayerGlobals : Node
{
	//Signal for special attack 3
	[Signal] public delegate void SpecialAttack3EventHandler();

	//Main player global variables
	public float MaxHealth, Range, AttackSpeed, Cooldown, Damage, Speed, JumpStrength, RollSpeed;
	public override void _Ready()
	{
		MaxHealth = 100;
		Range = 1;
		AttackSpeed = 1;
		Cooldown = 0.5f;
		Damage = 50;
		Speed = 300;
		JumpStrength = 700;
		RollSpeed = 600;
	}
}
