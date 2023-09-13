using Godot;
using System;

public partial class DamageIndicator : Node
{
	//Signal for handling the information of the indicators
	[Signal] public delegate void DamageIndicateEventHandler(CharacterBody2D damagedBody, float damageValue);

	//Scene variable for indicator body
	public PackedScene DamageIndicatorScene = GD.Load<PackedScene>("res://damage_indicator_label.tscn");

	//Variable for label
	Label DamageIndicatorBody = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DamageIndicate += OnDamageIndicate;
	}

	// Called every physics frame. 'delta' is the elapsed time since the previous physics frame.
	public override void _PhysicsProcess(double delta)
	{
	}

	/// <summary>
	/// Signal function for initialising the indicators
	/// </summary>
	public void OnDamageIndicate(CharacterBody2D damagedBody, float damageValue)
	{
		if (damagedBody != null)
		{
            DamageIndicatorBody = (Label)DamageIndicatorScene.Instantiate();
            damagedBody.GetParent().AddChild(DamageIndicatorBody);
			DamageIndicatorBody.GlobalPosition = damagedBody.GlobalPosition;
			DamageIndicatorBody.Text = damageValue.ToString();
		}
	}
}
