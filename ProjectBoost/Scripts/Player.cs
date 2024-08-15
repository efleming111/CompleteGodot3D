using Godot;
using System;
using System.Diagnostics;

public partial class Player : Node3D
{

    public override void _Ready()
    {
        Debug.Print("Ready Player");
    }

    public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_accept"))
		{
			Position = new Vector3(0.0f, Position.Y + (float)delta, 0.0f);
		}

        if (Input.IsActionPressed("ui_left"))
        {
            Rotation = new Vector3(0.0f, 0.0f, Rotation.Z + (float)delta);
        }

        if (Input.IsActionPressed("ui_right"))
        {
            Rotation = new Vector3(0.0f, 0.0f, Rotation.Z - (float)delta);
        }
    }
}
