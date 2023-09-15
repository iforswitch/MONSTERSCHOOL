using Godot;
using System;
using System.Security.AccessControl;

//Inherits State class
public partial class BasicEnemyChase : State
{
    //Export variable for player movement speed
    [Export] public float MovementSpeed;

    //Export variable for raycast
    [Export] public Node2D RaycastGroup = new();

    //Export variable for player jump
    [Export] public float JumpStrength;

    //Export variable for area2D
    [Export] public Area2D DetectionRadius = new();

    //Variable for movement direction
    public Vector2 Direction = new();

    //Variable for player reference
    public CharacterBody2D player = new();

    //variable for gravity
    public float gravity = 980;

    /// <summary>
    /// Function for entering the state
    /// </summary>
    public override void StateEnter()
    {
        GD.Print($"{Name} entered.");
        player = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
        DetectionRadius.Scale = new Vector2(2, 2);
        StateAnimation.Play(Name);
    }

    /// <summary>
    /// Function for exiting the state
    /// </summary>
    public override void StateExit()
    {
            
        GD.Print($"{Name} exited.");
    }

    /// <summary>
    /// Function for the physics of the state
    /// </summary>
    /// <param name="delta"></param>
    public override void PhysicsProcess(double delta)
    {
        Vector2 velocity = SubjectBody.Velocity;
    
        //Gravity
        if (!SubjectBody.IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        //Jump if there is a wall
        foreach (RayCast2D raycast in RaycastGroup.GetChildren())
        {
            if (raycast.IsColliding() && SubjectBody.IsOnFloor() && SubjectBody.GlobalPosition.DistanceTo(player.GlobalPosition) > 200)
            {
                velocity.Y = -JumpStrength;
            }
        }

        //Set direction to the player accordingly
        Direction = player.GlobalPosition - SubjectBody.GlobalPosition;
        if (Direction.X < 0)
        {
            Direction.X = -1;
            StateSprite.FlipH = true;
        }
        else if (Direction.X > 0) 
        {
            Direction.X = 1;
            StateSprite.FlipH = false;
        }

        //Set scale to direction
        RaycastGroup.Scale = new Vector2(Direction.X, 1);

        //Only move when close enough otherwise stop
        if (SubjectBody.GlobalPosition.DistanceTo(player.GlobalPosition) > 200)
        {
            velocity.X = Direction.X * MovementSpeed;
            StateAnimation.Play(Name);
        }
        else
        {
            velocity.X = 0;
            EmitSignal(signal: "StateTransition", this, "BasicEnemyAttack");
        }

        if (!DetectionRadius.HasOverlappingBodies())
        {
            EmitSignal(signal: "StateTransition", this, "BasicEnemyIdle");
        }

        PivotNode.Scale = new Vector2(Direction.X, 1);

        SubjectBody.Velocity = velocity;
    }

    /// <summary>
    /// Stop chasing the player if too far
    /// </summary>
    public void OnDetectionRadiusExited(Node2D player)
    {
        EmitSignal(signal: "StateTransition", this, "BasicEnemyIdle");
    }
}
