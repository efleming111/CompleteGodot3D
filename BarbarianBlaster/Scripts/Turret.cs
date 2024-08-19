using Godot;
using System;

public partial class Turret : Node3D
{
    [Export]
    private PackedScene projectile = null;

    public override void _Ready()
    {
        Area3D newProjectile = projectile.Instantiate<Area3D>();
        AddChild(newProjectile);
    }
}
