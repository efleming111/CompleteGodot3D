using Godot;
using System;
using System.Diagnostics;

public partial class Player : RigidBody3D
{
	[Export(PropertyHint.Range, "500.0f, 2000.0f")]
	private float thrust = 1000.0f;

    [Export(PropertyHint.Range, "50.0f, 200.0f")]
	private float torque = 100.0f;

	private bool isTransitioning = false;

    public override void _Process(double delta)
	{
		if (Input.IsActionPressed("thrust"))
		{
			ApplyCentralForce((float)delta * thrust * Basis.Y);
		}

		if (Input.IsActionPressed("rotateLeft"))
		{
			ApplyTorque(new Vector3(0.0f, 0.0f, torque * (float)delta));
		}

		if (Input.IsActionPressed("rotateRight"))
		{
            ApplyTorque(new Vector3(0.0f, 0.0f, -torque * (float)delta));
        }
	}

	public void OnBodyEntered(Node body)
	{
		if (!isTransitioning)
		{
			if(body.GetGroups().Contains("Goal"))
			{
				LevelComplete(((LandingPad)body).filePath);
			}

			if (body.GetGroups().Contains("Obstacle"))
			{
				ObstacleHit();
			}
		}
	}

	private void LevelComplete(String filePath)
	{
		isTransitioning = true;
        Tween tween = CreateTween();
        tween.TweenInterval(1.0f);
        tween.TweenCallback(Callable.From(() => GetTree().ChangeSceneToFile(filePath)));
        SetProcess(false);
    }

	private void ObstacleHit()
	{
        isTransitioning = true;
        Tween tween = CreateTween();
        tween.TweenInterval(1.0f);
		tween.TweenCallback(Callable.From(GetTree().ReloadCurrentScene));
		SetProcess(false);
    }
}
