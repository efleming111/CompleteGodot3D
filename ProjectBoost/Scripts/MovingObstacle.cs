using Godot;
using System;

public partial class MovingObstacle : AnimatableBody3D
{
    [Export]
    private Vector3 destination = Vector3.Zero;

    [Export]
    private float duration = 2.0f;

    public override void _Ready()
	{
		Tween tween = CreateTween();
        tween.SetLoops();
        tween.SetTrans(Tween.TransitionType.Sine);
		tween.TweenProperty(this, "global_position", GlobalPosition + destination, duration);
        tween.TweenProperty(this, "global_position", GlobalPosition, duration);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
