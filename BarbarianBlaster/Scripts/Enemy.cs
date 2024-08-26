using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export(PropertyHint.Range, "1.0f, 100.0f")]
	private float walkSpeed = 5.0f;

	[Export]
	private float maxHealth = 50.0f;

	private Base levelBase = null;
	private AnimationPlayer player;
    private Bank bank = null;

    private float currentHealth;
	public float CurrentHealth
	{
		get { return currentHealth; }
	}

    public override void _Ready()
    {
		levelBase = (Base)GetTree().GetFirstNodeInGroup("Base");
		player = GetNode<AnimationPlayer>("AnimationPlayer");
        bank = GetTree().GetFirstNodeInGroup("Bank") as Bank;
        currentHealth = maxHealth;
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
	
	public void TakeDamage(float damagePoints)
	{
		currentHealth -= damagePoints;
        player.Play("HitDamage");
        if (currentHealth < 1.0f)
        {
			bank.CollectGold(15);
            QueueFree();
        }
        
	}
}
