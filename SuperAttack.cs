using Godot;
using System;

public partial class SuperAttack : State
{
    //Export variable for rolling speed
    [Export] public float RollSpeed;

    //Export variable for effects
    [Export] public Node2D EffectNodes = new();

    //Variable for movement direction
    public Vector2 Direction = new();

    //variable for gravity
    public float gravity = 980;

    //variable for super attack
    PackedScene SuperAttackScene = GD.Load<PackedScene>("res://super_attack.tscn");

    //Local timer variable
    public Timer SuperAttackCooldown = new();

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        StateAnimation.Play(Name);
        SuperAttackCooldown = RollCooldown;
        CharacterBody2D parent = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        Node2D instance = (Node2D)SuperAttackScene.Instantiate();
        instance.GlobalPosition = parent.GlobalPosition;
        instance.Scale = new Vector2(instance.Scale.X * PivotNode.Scale.X, instance.Scale.Y); 
        GetParent().GetParent().GetParent().AddChild(instance);
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
        GD.Print($"{Name} exited.");

        //Cooldown start
        SuperAttackCooldown.Start();
    }

    /// <summary>
    /// Function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = SubjectBody.Velocity;
        velocity.Y += gravity * (float)delta;

        //Sprite direction check
        if (PivotNode.Scale.X == -1)
        {
            Direction = Vector2.Left;
            StateSprite.FlipH = true;
        }

        if (PivotNode.Scale.X == 1)
        {
            Direction = Vector2.Right;
            StateSprite.FlipH = false;
        }

        //Roll a distance
        if (Direction.X == 0)
        {
            velocity.X = RollSpeed * PivotNode.Scale.X;
        }
        else
        {
            velocity.X = RollSpeed * Direction.X;
        }

        if (Direction.X != 0)
        {
            PivotNode.Scale = new Vector2(Direction.X, 1);
        }

        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Function to check when the attack animation has finished
    /// </summary>
    /// <param name="anim_name"></param>
    public void OnAnimationPlayerFinished(string anim_name)
    {
        //Go to idle
        if (anim_name == Name)
        {
            EmitSignal(signal: "StateTransition", this, "PlayerIdle");
        }
    }
}
