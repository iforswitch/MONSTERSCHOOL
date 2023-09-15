using Godot;
using System;

public partial class game_over : Control
{
    /// <summary>
    /// Function called when start pressed
    /// </summary>
    public void OnStartPressed()
    {
        GetTree().ChangeSceneToFile("res://start_screen.tscn");
    }

    /// <summary>
    /// Function called when exit pressed
    /// </summary>
    public void OnExitPressed()
    {
        GetTree().Quit();
    }
}
