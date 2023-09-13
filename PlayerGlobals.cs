using Godot;
using System;

public partial class PlayerGlobals : Node
{
	//Signal for special attack 3
	[Signal] public delegate void SpecialAttack3EventHandler();

	//Variable for checking if special attack 3 is activated
	public int IsSpecialAttack3;

	//Main player global variables
	public float MaxHealth, Range, AttackSpeed, Cooldown, Damage, Speed, JumpStrength, RollSpeed;
	public override void _Ready()
	{
		IsSpecialAttack3 = -1;
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
