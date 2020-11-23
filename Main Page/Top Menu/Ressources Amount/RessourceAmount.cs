using Godot;
using System;

public class RessourceAmount : HBoxContainer
{
    [Export]
    private string RessourceType;
    Label value;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        value = GetNode<Label>("Value");

        // Set the ressource amount on load
        switch (RessourceType)
        {      
            case "Wood":          
                value.Text = GlobalVariables.WoodAmount.ToString();
                break;
            case "Stone":
                value.Text = GlobalVariables.StoneAmount.ToString();
                break;
            case "Food":
                value.Text = GlobalVariables.FoodAmount.ToString();
                break;
            case "Gold":
                value.Text = GlobalVariables.GoldAmount.ToString();                            
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
                value.Text = GlobalVariables.WoodAmount.ToString();
                break;
            case "Stone":
                value.Text = GlobalVariables.StoneAmount.ToString();
                break;
            case "Food":
                value.Text = GlobalVariables.FoodAmount.ToString();
                break;
            case "Gold":
                value.Text = GlobalVariables.GoldAmount.ToString();
                break;
            default:
                break;
        }
    }
}
