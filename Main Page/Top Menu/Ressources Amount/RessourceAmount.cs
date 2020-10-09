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

        // Set the ressource amount on load
        switch (RessourceType)
        {      
            case "Wood":          
                GetNode<Label>("Value").Text = globalVariables.WoodAmount.ToString();
                break;
            case "Stone":
                GetNode<Label>("Value").Text = globalVariables.StoneAmount.ToString();
                break;
            case "Food":
                GetNode<Label>("Value").Text = globalVariables.FoodAmount.ToString();
                break;
            case "Gold":
                GetNode<Label>("Value").Text = globalVariables.GoldAmount.ToString();                            
                break;
            default:
                break;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        // Update the amount every frame
        switch (RessourceType)
        {
            case "Wood":
                GetNode<Label>("Value").Text = globalVariables.WoodAmount.ToString();
                break;
            case "Stone":
                GetNode<Label>("Value").Text = globalVariables.StoneAmount.ToString();
                break;
            case "Food":
                GetNode<Label>("Value").Text = globalVariables.FoodAmount.ToString();
                break;
            case "Gold":
                GetNode<Label>("Value").Text = globalVariables.GoldAmount.ToString();
                break;
            default:
                break;
        }
    }
}
