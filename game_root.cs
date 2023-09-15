using Godot;
using System;

public partial class game_root : Node2D
{
    public PackedScene StartScene = GD.Load<PackedScene>("res://test_scene.tscn");
    public PlayerGlobals PlayerGlobalsVariable = new();


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        PlayerGlobalsVariable._Ready();
        var StartSceneInstance = StartScene.Instantiate();
        AddChild(StartSceneInstance);
	}
}
