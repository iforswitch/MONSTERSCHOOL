using Godot;
using System;

public partial class PlayerGlobals : Node
{
	//Signal for special attack 3
	[Signal] public delegate void SpecialAttack3EventHandler();

	[Signal] public delegate void LevelUpEventHandler();

	//Variable for checking if special attack 3 is activated
	public int IsSpecialAttack3;

	//Main player global variables
	public float MaxHealth, CurrentHealth, AttackSpeed, Cooldown, Damage, Speed, JumpStrength, RollSpeed;

	//Stat player global variables
	public float AttackStat, DexterityStat, HealthStat;
	public override void _Ready()
	{
		//Main variables
		IsSpecialAttack3 = -1;
		MaxHealth = 100;
		CurrentHealth = MaxHealth;
		AttackSpeed = 1;
		Cooldown = 0.5f;
		Damage = 50;
		Speed = 300;
		JumpStrength = 700;
		RollSpeed = 600;

		//Stat Variables
		AttackStat = 0;
		DexterityStat = 0;
		HealthStat = 0;
	}
}
