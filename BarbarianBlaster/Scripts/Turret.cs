using Godot;
using System.Diagnostics;
using System.Linq;

public partial class Turret : Node3D
{
    [Export]
    private PackedScene projectile = null;

    [Export]
    private float turretRange = 10.0f;

    private Node3D projectileSpawn = null;
    private PathFollow3D enemy = null;
    private AnimationPlayer player;

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
        player = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        enemy = FindBestTarget();
        if (enemy != null) 
        { 
            LookAt(enemy.GlobalPosition, Vector3.Up, true);
        }
    }

    public void OnTimerTimeout()
    {
        if(enemy != null)
        {
            Projectile newProjectile = projectile.Instantiate<Projectile>();
            AddChild(newProjectile);
            newProjectile.GlobalPosition = projectileSpawn.GlobalPosition;
            newProjectile.Direction = GlobalTransform.Basis.Z;
            player.Play("TurretFire");
        }
    }

    public PathFollow3D FindBestTarget()
    {
        PathFollow3D bestTarget = null;
        float bestProgress = 0.0f;
        Godot.Collections.Array<Node> pathChildren = enemyPath.GetChildren();
        
        for(int i = 0; i < pathChildren.Count; i++)
        {
            if (pathChildren[i].IsInGroup("EnemyArea"))
            {
                PathFollow3D currentEnemy = pathChildren[i] as PathFollow3D;
                float distanceToEnemy = GlobalPosition.DistanceTo(currentEnemy.GlobalPosition);
                if(currentEnemy.ProgressRatio > bestProgress && distanceToEnemy < turretRange)
                {
                    bestProgress = currentEnemy.ProgressRatio;
                    bestTarget = currentEnemy;
                }
            }
        }

        return bestTarget;
    }
}
