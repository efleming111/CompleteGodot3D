using Godot;
using System;
using System.Diagnostics;
using static Godot.TabContainer;

public partial class RayPicker : Camera3D
{
	[Export]
	private GridMap gridMap = null;

	[Export]
	private TurretManager turretManager = null;

	private RayCast3D rayCast = null;
	private Bank bank = null;

	public override void _Ready()
	{
		rayCast = GetNode<RayCast3D>("RayCast3D");
		bank = GetTree().GetFirstNodeInGroup("Bank") as Bank;
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
				if (bank.CanSpendGold(turretManager.TurrentCost))
				{
					Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);

					if (Input.IsActionJustPressed("click"))
					{
						Vector3 collisionPoint = rayCast.GetCollisionPoint();
						Vector3I mapPosition = gridMap.LocalToMap(collisionPoint);
						if (gridMap.GetCellItem(mapPosition) == 0)
						{
							gridMap.SetCellItem(mapPosition, 1);
							turretManager.BuildTurret(gridMap.MapToLocal(mapPosition));
							bank.SpendGold(turretManager.TurrentCost);
						}
					}
				}
                else
                {
                    Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
                }
            }
			else
			{
                Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
            }
		}
	}
}
