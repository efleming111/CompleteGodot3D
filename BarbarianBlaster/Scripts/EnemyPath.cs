using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class EnemyPath : Path3D
{
    [Export]
    private PackedScene enemy = null;

    public void SpawnEnemy()
    {
        PathFollow3D newEnemy = enemy.Instantiate<PathFollow3D>();
        AddChild(newEnemy);
        newEnemy.ProgressRatio = 0.0f;
    }

    public void OnTimerTimeout()
    {
        SpawnEnemy();
    }
}
