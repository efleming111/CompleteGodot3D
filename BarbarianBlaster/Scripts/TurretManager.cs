using Godot;
using System;

public partial class TurretManager : Node3D
{
	[Export]
	private PackedScene turret = null;

	[Export]
	private Path3D enemyPath = null;

    [Export]
    private int turretCost = 100;
	public int TurrentCost
	{
		get { return turretCost; }
	}


    public void BuildTurret(Vector3 position)
	{
        Turret newTurret = turret.Instantiate<Turret>();
        AddChild(newTurret);
		newTurret.GlobalPosition = position;
		newTurret.EnemyPath = enemyPath;
    }
}
