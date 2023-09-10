using Godot;
using System;

public partial class EnemyHitbox : Area2D
{
	[Signal] public delegate void EOverlappingBodiesCheckEventHandler();

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (Monitoring)
        {
            if (HasOverlappingBodies())
            {
                EmitSignal("EOverlappingBodiesCheck", GetOverlappingBodies());
            }
        }
    }
}
