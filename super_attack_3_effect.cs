using Godot;
using System;

public partial class super_attack_3_effect : Node2D
{
    //Export Variable for animation player
    [Export] public AnimationPlayer AnimPlay = new();

    //Export variable for tmer
    [Export] public Timer t = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AnimPlay.Play("SuperAttack3Effect");
    }

    /// <summary>
    /// Function to check initial animation finished
    /// </summary>
    /// <param name="anim_name"></param>

    public void OnAnimationPlayerFinished(string anim_name)
    {
        //Animation finish to queue free entire attack tree
        /*if (anim_name == Name)
        {
            AnimPlay.Play("SuperAttackProjectileFade");
        }
        else if (anim_name == "SuperAttackProjectileFade")
        {
            QueueFree();
        }*/

    }

    public void _on_timer_timeout()
    {
        //AnimPlay.Play("SuperAttackProjectileFade");
    }
}
