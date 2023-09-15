using Godot;
using System;

public partial class start_screen : Control
{
    [Export] public TextEdit UsernameField = new();
    public PlayerGlobals PlayerGlobalsVariable = new();

    public override void _Ready()
    {
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");
    }

    /// <summary>
    /// Function called when start pressed
    /// </summary>
    public void OnStartPressed()
	{
        if (UsernameField.Text != "" || UsernameField.Text != null)
        {
            UsernameField.Text = "Unknown";
            GetTree().ChangeSceneToFile("res://test_scene.tscn");
        }
    }

	public void _on_text_edit_text_changed()
	{
        PlayerGlobalsVariable.Username = UsernameField.Text;
        GD.Print(PlayerGlobalsVariable.Username);
	}

    /// <summary>
    /// Function called when exit pressed
    /// </summary>
    public void OnExitPressed()
	{
		GetTree().Quit();
	}
}
