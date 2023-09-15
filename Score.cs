using Godot;
using System;

public partial class Score : Label
{
	//Player global variables
	public PlayerGlobals PlayerGlobalsVariable = new();

	//Variable for skeleton container
	public Node2D SkeletonContainer = new();

	//Variable for number of skeletons
	public float SkeletonCount;

	//Game switch
	public bool IsPlaying = false;

	//Export variable for wave timer done
	[Export] public Timer WaveDoneTimer = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Set global player variables
		PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");

		//Set skeleton container variable
		SkeletonContainer = (Node2D)GetTree().GetFirstNodeInGroup("SkeletonContainer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (!PlayerGlobalsVariable.IsPlaying) 
		{
            PlayerGlobalsVariable.IsPlaying = true;

            //Set skeleton count
            OnWaveReset();
        }

		//Set text
		if (PlayerGlobalsVariable.SkeletonsKilled != SkeletonCount)
		{
            Text = $"Wave: {PlayerGlobalsVariable.Wave}\n {PlayerGlobalsVariable.SkeletonsKilled}/{SkeletonCount}";
        }

		//Finished wave
		if (PlayerGlobalsVariable.SkeletonsKilled == SkeletonCount && !PlayerGlobalsVariable.IsLevelling)
		{
			PlayerGlobalsVariable.IsLevelling = true;
            WaveDoneTimer.Start();
            Text = $"Wave: {PlayerGlobalsVariable.Wave}\n Wave Cleared!";
        }
	}

	/// <summary>
	/// Function called after every new wave
	/// </summary>
	public void OnWaveReset()
	{
		SkeletonCount = SkeletonContainer.GetChildCount();
		GD.Print(SkeletonCount);
	}

	/// <summary>
	/// Function called when wave finished timer timeouts
	/// </summary>
	public void OnWaveDoneTimerTimeout()
	{
		//Level up
        PlayerGlobalsVariable.EmitSignal("LevelUp");
    }
}
