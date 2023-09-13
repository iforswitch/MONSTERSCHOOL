using Godot;
using System;

public partial class super_attack_body : Node2D
{
    /// <summary>
    /// Executes every physics frame
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        //Destroy self when there is no more children
        if (GetChildren().Count == 0)
        {
            QueueFree();
        }
    }
}
