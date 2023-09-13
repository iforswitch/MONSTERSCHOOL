using Godot;
using System;

public partial class SuperAttackHitbox : Area2D
{
    //Signal for checking multiple bodies on collision
    [Signal] public delegate void OverlappingBodiesCheckEventHandler();

    /// <summary>
    /// Called every physics frame
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        if (Monitoring)
        {
            if (HasOverlappingBodies())
            {
                EmitSignal("OverlappingBodiesCheck", GetOverlappingBodies());
            }
        }
    }
}
