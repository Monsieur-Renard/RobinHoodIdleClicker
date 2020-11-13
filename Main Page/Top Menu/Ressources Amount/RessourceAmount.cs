using Godot;
using System;

public class RessourceAmount : HBoxContainer
{
    [Export]
    private string RessourceType;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Set the ressource amount on load
        switch (RessourceType)
        {      
            case "Wood":          
                GetNode<Label>("Value").Text = GlobalVariables.WoodAmount.ToString();
                break;
            case "Stone":
                GetNode<Label>("Value").Text = GlobalVariables.StoneAmount.ToString();
                break;
            case "Food":
                GetNode<Label>("Value").Text = GlobalVariables.FoodAmount.ToString();
                break;
            case "Gold":
                GetNode<Label>("Value").Text = GlobalVariables.GoldAmount.ToString();                            
                break;
            default:
                break;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Update the amount every frame
        switch (RessourceType)
        {
            case "Wood":
                GetNode<Label>("Value").Text = GlobalVariables.WoodAmount.ToString();
                break;
            case "Stone":
                GetNode<Label>("Value").Text = GlobalVariables.StoneAmount.ToString();
                break;
            case "Food":
                GetNode<Label>("Value").Text = GlobalVariables.FoodAmount.ToString();
                break;
            case "Gold":
                GetNode<Label>("Value").Text = GlobalVariables.GoldAmount.ToString();
                break;
            default:
                break;
        }
    }
}
