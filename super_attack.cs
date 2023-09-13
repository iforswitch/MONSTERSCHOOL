using Godot;

public partial class super_attack : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

    //variable for super attack projectile
    PackedScene SuperAttackProjectile = GD.Load<PackedScene>("res://super_attack_projectile.tscn");

    //Export variable for damage
    [Export] public float damage;

    //Export variable for raycast
    [Export] public RayCast2D raycast = new();

    //xport variable for parent
    public Node2D parent = new ();

    //Variable to check if hit is available
    public bool CanHit;

    public override void _Ready()
    {
        CanHit = true;
        parent = (Node2D)GetParent();
    }

    public override void _PhysicsProcess(double delta)
	{
        Vector2 velocity = Velocity;
        velocity.X = Speed * 2 * parent.Scale.X;

        //Checking for wall collisions to start destroying self
        if (raycast.IsColliding())
        {
            QueueFree();
        }

        Velocity = velocity;
		MoveAndSlide();
	}

	/// <summary>
	/// Timer function for super attack projection
	/// </summary>
	public void OnTimerTimeout()
	{
		if (SuperAttackProjectile != null)
		{
            Node2D instance = (Node2D)SuperAttackProjectile.Instantiate();
            GetParent().AddChild(instance);
            instance.GlobalPosition = GlobalPosition;
		}	
    }

    /// <summary>
    /// Function for hit detection on multiple enemies
    /// </summary>
    /// <param name="bodyList"></param>
    public void OnOverlappingBodiesCheck(Godot.Collections.Array<Node2D> bodyList)
    {
        if (bodyList != null && CanHit)
        {
            CanHit = false;
            foreach (var body in bodyList)
            {
                for (int i = 0; i < body.GetChildCount(); i++)
                {
                    if (body.GetChild(i) is FSM)
                    {
                        FSM enemyFSM = body.GetChild(i) as FSM;
                        enemyFSM.Hit(damage);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Function for hit detection on single enemies
    /// </summary>
    /// <param name="body"></param>
    public void SuperAttackHitboxEntered(CharacterBody2D body) 
    {
        if (!CanHit)
        {
            CanHit = true;  
        }
        if (body != null && CanHit)
        {
            CanHit = false; 
            for (int i = 0; i < body.GetChildCount(); i++)
            {
                if (body.GetChild(i) is FSM)
                {
                    FSM enemyFSM = body.GetChild(i) as FSM;
                    enemyFSM.Hit(damage);
                }
            }
        }
    }
}
