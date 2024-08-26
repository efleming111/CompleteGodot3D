using Godot;
using System;

public partial class Bank : MarginContainer
{
    [Export]
    private int startingGold = 150;

    private Label goldLabel = null;

    private int gold;
    public int Gold
    {
        get { return gold; }
    }

    public override void _Ready()
    {
        base._Ready();
        goldLabel = GetNode<Label>("GoldLabel");
        UpdateGold(startingGold);
    }

    public void CollectGold(int goldAmount)
    {
        gold += goldAmount;
        UpdateGold(gold);
    }

    public void SpendGold(int goldAmount)
    {
        if (CanSpendGold(goldAmount)) 
        {
            gold -= goldAmount;
            UpdateGold(gold);
        }
    }

    public bool CanSpendGold(int goldAmount)
    {
        return gold - goldAmount >= 0;
    }

    private void UpdateGold(int goldAmount)
    {
        gold = goldAmount;
        goldLabel.Text = "Gold: " + gold;
    }
}
