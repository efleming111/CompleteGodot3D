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
	private AudioStreamPlayer explosionSoundFx = null;
    private AudioStreamPlayer successSoundFx = null;
    private AudioStreamPlayer3D rocketSoundFx = null;

	private GpuParticles3D mainBoosterParticle = null;
    private GpuParticles3D leftBoosterParticle = null;
    private GpuParticles3D rightBoosterParticle = null;
    private GpuParticles3D explosionParticle = null;
    private GpuParticles3D successParticle = null;

    public override void _Ready()
    {
		explosionSoundFx = GetNode<AudioStreamPlayer>("ExplosionSoundFx");
        successSoundFx = GetNode<AudioStreamPlayer>("SuccessSoundFx");
		rocketSoundFx = GetNode<AudioStreamPlayer3D>("RocketSoundFx");

        mainBoosterParticle = GetNode<GpuParticles3D>("MainBoosterParticle");
        leftBoosterParticle = GetNode<GpuParticles3D>("RightBoosterParticle");
        rightBoosterParticle = GetNode<GpuParticles3D>("LeftBoosterParticle");
        explosionParticle = GetNode<GpuParticles3D>("ExplosionParticle");
        successParticle = GetNode<GpuParticles3D>("SuccessParticle");
    }

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Quit();
		}

		if (Input.IsActionPressed("thrust"))
		{
			ApplyCentralForce((float)delta * thrust * Basis.Y);
			mainBoosterParticle.Emitting = true;
			if (!rocketSoundFx.Playing)
			{
				rocketSoundFx.Play();
			}
		}
		else
		{
			mainBoosterParticle.Emitting = false;
			rocketSoundFx.Stop();
		}

		if (Input.IsActionPressed("rotateLeft"))
		{
			ApplyTorque(new Vector3(0.0f, 0.0f, torque * (float)delta));
            leftBoosterParticle.Emitting = true;
        }
		else
		{
            leftBoosterParticle.Emitting = false;
        }

		if (Input.IsActionPressed("rotateRight"))
		{
			ApplyTorque(new Vector3(0.0f, 0.0f, -torque * (float)delta));
            rightBoosterParticle.Emitting = true;
        }
		else
		{
            rightBoosterParticle.Emitting = false;
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

	private void TurnOffAllBoostParticlesAndThrustSoundFx()
	{
        mainBoosterParticle.Emitting = false;
        leftBoosterParticle.Emitting = false;
        rightBoosterParticle.Emitting = false;
        rocketSoundFx.Stop();
    }

	private void LevelComplete(String filePath)
	{
		successSoundFx.Play();
		successParticle.Emitting = true;
        TurnOffAllBoostParticlesAndThrustSoundFx();
        isTransitioning = true;
        Tween tween = CreateTween();
        tween.TweenInterval(2.5f);
        tween.TweenCallback(Callable.From(() => GetTree().ChangeSceneToFile(filePath)));
        SetProcess(false);
    }

	private void ObstacleHit()
	{
		explosionSoundFx.Play();
        explosionParticle.Emitting = true;
        TurnOffAllBoostParticlesAndThrustSoundFx();
        isTransitioning = true;
        Tween tween = CreateTween();
        tween.TweenInterval(2.5f);
		tween.TweenCallback(Callable.From(GetTree().ReloadCurrentScene));
		SetProcess(false);
    }
}
