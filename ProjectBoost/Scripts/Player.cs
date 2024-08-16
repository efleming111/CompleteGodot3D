using Godot;
using System;
using System.Diagnostics;

public partial class Player : RigidBody3D
{

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_accept"))
		{
			ApplyCentralForce((float)delta * 1000.0f * Basis.Y);
		}

		if (Input.IsActionPressed("ui_left"))
		{
			ApplyTorque(new Vector3(0.0f, 0.0f, 100.0f * (float)delta));
		}

		if (Input.IsActionPressed("ui_right"))
		{
            ApplyTorque(new Vector3(0.0f, 0.0f, -100.0f * (float)delta));
        }
	}
}
