using Godot;
using System;

public partial class PlayerGlobals : Node
{
	//Signal for special attack 3
	[Signal] public delegate void SpecialAttack3EventHandler();

	//Signal for levelling up
	[Signal] public delegate void LevelUpEventHandler();

	//Variable for checking if special attack 3 is activated
	public int IsSpecialAttack3;

	//Main player global variables
	public float MaxHealth, CurrentHealth, AttackSpeed, Cooldown, Damage, Speed, JumpStrength, RollSpeed;

	//Stat player global variables
	public float AttackStat, DexterityStat, HealthStat;

	//Bool to check for levelling up
	public bool IsLevelling;

	//Score
	public int Wave;

	//Skeletons Killed
	public int SkeletonsKilled;

	//Isplaying
	public bool IsPlaying;

	//String highscore
	public string HighScore = "";

	//Player Username
	public string Username = "";
	public override void _Ready()
	{
        //Write highscore wave to file
        var file = FileAccess.Open("../SkeletonBananza", FileAccess.ModeFlags.Read);
		HighScore = file.GetAsText();

		Username = "";

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

		//Levelling
		IsLevelling = false;

		//Score
		Wave = 1;

		//Skeletons Killed
		SkeletonsKilled = 0;

		//Isplaying
		IsPlaying = false;
	}
}
