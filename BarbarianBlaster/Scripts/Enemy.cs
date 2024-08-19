using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export(PropertyHint.Range, "1.0f, 100.0f")]
	private float walkSpeed = 5.0f;

	private Base levelBase = null;

    public override void _Ready()
    {
		levelBase = (Base)GetTree().GetFirstNodeInGroup("Base");
    }

    public override void _Process(double delta)
	{
		Progress += (float)delta * walkSpeed;

		if(ProgressRatio > .99f)
		{
			levelBase.TakeDamage();
			SetProcess(false);
		}

	}
}
