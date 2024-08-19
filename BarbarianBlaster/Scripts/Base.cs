using Godot;
using System;
using System.Diagnostics;

public partial class Base : Node3D
{
	[Export]
	private int startingHealth = 5;

	private Label3D healthLabel = null;

	private int currentHealth;
	public int CurrentHealth
	{
		get { return currentHealth; }
		set { 
			currentHealth = value;
            healthLabel.Text = currentHealth + "/" + startingHealth;
			float weight = 1.0f - (float)currentHealth / (float)startingHealth;
			healthLabel.Modulate = Colors.Green.Lerp(Colors.Red, weight);
			if(currentHealth <= 0)
			{
				GetTree().ReloadCurrentScene();
			}
        }
	}

	public override void _Ready()
	{
		healthLabel = GetNode<Label3D>("HealthLabel");
        CurrentHealth = startingHealth;
	}

	public void TakeDamage()
	{
        CurrentHealth--;
	}
}
