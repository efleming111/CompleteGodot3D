using Godot;
using System;

public partial class Projectile : Area3D
{
    [Export]
    private float speed = 30.0f;

    private Vector3 direction = Vector3.Forward;
    public Vector3 Direction
    {
        get { return direction; }
        set
        {
            direction = value;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Translate((float)delta * speed * direction);
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }
}
