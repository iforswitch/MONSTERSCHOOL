using Godot;
using System;

public partial class super_attack_projectile : Node2D
{
    //variable for animation player
    [Export] public AnimationPlayer AnimPlay = new();

    [Export] public Timer t = new();

    //Variable for main body raycast
    RayCast2D MainBodyRaycast = new();

    //Variable for stopping loop
    public bool Colliding;

    //Variable for global signal
    SuperAttackGlobal superAttackGlobal = new();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //superAttackGlobal = GetNode<SuperAttackGlobal>("/root/SuperAttackGlobal");
        //superAttackGlobal.Connect("SuperAttackFinished", Callable.From(OnSuperAttackFinished));
        AnimPlay.Play("SuperAttackProjectile");
    }

    /// <summary>
    /// Signal function for super attack finishing
    /// </summary>
    public void OnSuperAttackFinished()
    {
        //AnimPlay.Play("SuperAttackProjectileFade");
    }

    /// <summary>
    /// Function to check initial animation finished
    /// </summary>
    /// <param name="anim_name"></param>

    public void OnAnimationPlayerFinished(string anim_name)
    {
        //Animation finish to queue free entire attack tree
        if (anim_name == "SuperAttackProjectile")
        {
            AnimPlay.Play("SuperAttackProjectileFade");
        }
        else if (anim_name == "SuperAttackProjectileFade")
        {
            QueueFree();
        }

    }

    public void _on_timer_timeout()
    {
        AnimPlay.Play("SuperAttackProjectileFade");
    }
}
