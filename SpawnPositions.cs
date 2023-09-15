using Godot;
using System;

public partial class SpawnPositions : Node2D
{
	[Signal] public delegate void WaveResetEventHandler();

	//Variable for number of skeletons
	public float SkeletonNumber;

	//Variable for random spawns
	public float RandomSpawn;

	//Variable for skeleton scene
	PackedScene SkeletonScene = GD.Load<PackedScene>("res://basic_enemy.tscn");

	//Variable for skeleton container
	Node2D SkeletonContainer = new();

	//Variable for position
	Node2D position = new();

	//Variable for random
	public RandomNumberGenerator rng = new();

	//Variable for player global variables
	PlayerGlobals PlayerGlobalsVariable = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Connect signal
		WaveReset += OnWaveReset;

		//Set skeleton container
		SkeletonContainer = (Node2D)GetTree().GetFirstNodeInGroup("SkeletonContainer");

		//Set player global variables
		PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");

        //Set skeleton number
        SkeletonNumber = CalculateWave(PlayerGlobalsVariable.Wave);

        //Set skeleton position randomly
        for (int i = 0; i < SkeletonNumber; i++)
		{
			RandomSpawn = rng.RandiRange(-1, 4);
            position = (Node2D)GetChild((int)RandomSpawn);
            CharacterBody2D instance = (CharacterBody2D)SkeletonScene.Instantiate();
			SkeletonContainer.AddChild(instance);
			instance.GlobalPosition = position.GlobalPosition;
		}
	}

	//Calculate wave enemy numbers
	public float CalculateWave(float waveNumber)
	{
		float number = 1 + (2 * (waveNumber));
		return number;
	}

	//Reset the wave into a new wave
	public void OnWaveReset()
	{
		//Reset skeletons killed
		PlayerGlobalsVariable.SkeletonsKilled = 0;

        //Increase wave by 1
        PlayerGlobalsVariable.Wave += 1;

		//Isplaying is false
		PlayerGlobalsVariable.IsPlaying = false;

		//Isleveling is false
		PlayerGlobalsVariable.IsLevelling = false;

		//Heal half health
		PlayerGlobalsVariable.CurrentHealth += PlayerGlobalsVariable.CurrentHealth / 2;

		//No overhealing
		if (PlayerGlobalsVariable.CurrentHealth > PlayerGlobalsVariable.MaxHealth)
		{
			PlayerGlobalsVariable.CurrentHealth = PlayerGlobalsVariable.MaxHealth;
		}

		//Reload wave
        GetTree().ReloadCurrentScene();
    }
}
