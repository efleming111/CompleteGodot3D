using Godot;
using System;
using System.Diagnostics;

public partial class RayPicker : Camera3D
{
	[Export]
	private GridMap gridMap = null;

	[Export]
	private TurretManager turretManager = null;

	private RayCast3D rayCast = null;

	public override void _Ready()
	{
		rayCast = GetNode<RayCast3D>("RayCast3D");
	}

	public override void _Process(double delta)
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();
		rayCast.TargetPosition = ProjectLocalRayNormal(mousePosition) * 100.0f;
		rayCast.ForceRaycastUpdate();

		if (rayCast.IsColliding())
		{
			GodotObject collider = rayCast.GetCollider();
			if(collider is GridMap)
			{
				Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);

				if (Input.IsActionJustPressed("click"))
				{
					Vector3 collisionPoint = rayCast.GetCollisionPoint();
					Vector3I mapPosition =  gridMap.LocalToMap(collisionPoint);
					if(gridMap.GetCellItem(mapPosition) == 0)
					{
						gridMap.SetCellItem(mapPosition, 1);
						turretManager.BuildTurret(gridMap.MapToLocal(mapPosition));
					}
				}
			}
			else
			{
                Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
            }
		}
	}
}
