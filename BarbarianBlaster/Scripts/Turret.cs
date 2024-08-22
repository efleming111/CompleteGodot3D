using Godot;
using System.Diagnostics;
using System.Linq;

public partial class Turret : Node3D
{
    [Export]
    private PackedScene projectile = null;

    private Node3D projectileSpawn = null;

    private Path3D enemyPath;
    public Path3D EnemyPath
    {
        get { return enemyPath; }
        set
        {
            enemyPath = value;
        }
    }

    public override void _Ready()
    {
        projectileSpawn = GetNode<Node3D>("TurretBase/TurretTop/BarrelAssembly/BarrelTip/ProjectileSpawn");
    }

    public override void _PhysicsProcess(double delta)
    {
        Enemy enemy = enemyPath.GetChildren().Last<Node>() as Enemy;
        LookAt(enemy.GlobalPosition, Vector3.Up, true);
    }

    public void OnTimerTimeout()
    {
        Projectile newProjectile = projectile.Instantiate<Projectile>();
        AddChild(newProjectile);
        newProjectile.GlobalPosition = projectileSpawn.GlobalPosition;
        newProjectile.Direction = GlobalTransform.Basis.Z;
    }
}
