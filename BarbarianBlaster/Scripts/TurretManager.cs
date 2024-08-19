using Godot;
using System;

public partial class TurretManager : Node3D
{
	[Export]
	private PackedScene turret = null;

	public void BuildTurret(Vector3 position)
	{
        Node3D newTurret = turret.Instantiate<Node3D>();
        AddChild(newTurret);
		newTurret.GlobalPosition = position;
    }
}
