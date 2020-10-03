using Godot;
using System;

public class RessourceAmount : HBoxContainer
{
    [Export]
    private string RessourceType;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        switch (RessourceType)
        {      
            case "wood":          
                GetNode<Label>("Value").Text = globalVariables.WoodAmount.ToString();
                var buildingNode = GetTree().Root.FindNode("BuildingWood", true, false);
                buildingNode.Connect("WoodChange", this, "OnWoodAmountChange");
                break;
            case "stone":
                GetNode<Label>("Value").Text = globalVariables.StoneAmount.ToString();
                buildingNode = GetTree().Root.FindNode("BuildingStone", true, false);
                buildingNode.Connect("StoneChange", this, "OnStoneAmountChange");
                break;
            case "food":
                GetNode<Label>("Value").Text = globalVariables.FoodAmount.ToString();
                buildingNode = GetTree().Root.FindNode("BuildingFood", true, false);
                buildingNode.Connect("FoodChange", this, "OnFoodAmountChange");
                break;
            case "gold":
                GetNode<Label>("Value").Text = globalVariables.GoldAmount.ToString();              
                buildingNode = GetTree().Root.FindNode("BuildingGold", true, false);
                buildingNode.Connect("GoldChange", this, "OnGoldAmountChange");
                break;
            default:
                break;
        }
    }

    public void OnWoodAmountChange()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");
        GetNode<Label>("Value").Text = globalVariables.WoodAmount.ToString();
    }

    public void OnStoneAmountChange()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");
        GetNode<Label>("Value").Text = globalVariables.StoneAmount.ToString();
    }

    public void OnGoldAmountChange()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");
        GetNode<Label>("Value").Text = globalVariables.GoldAmount.ToString();
    }

    public void OnFoodAmountChange()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");
        GetNode<Label>("Value").Text = globalVariables.FoodAmount.ToString();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
