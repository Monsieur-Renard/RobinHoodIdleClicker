using Godot;
using System;

public class RessourceGain : HBoxContainer
{
    [Export]
    private string RessourceType;
    Label number;
    double previousAmount;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        number = GetNode<Label>("Number");
        
        // Set the ressource amount on load
        switch (RessourceType)
        {
            case "Wood":
                number.Text = "0";
                break;
            case "Stone":
                number.Text = "0";
                break;
            case "Food":
                number.Text = "0";
                break;
            case "Gold":
                number.Text = "0";
                break;
            default:
                break;
        }
    }

    public void OnTimerTimeout()
    {
        // Update the amount every frame
        switch (RessourceType)
        {
            case "Wood":
                number.Text = (GlobalVariables.WoodAmount - previousAmount).ToString();
                previousAmount = GlobalVariables.WoodAmount;
                break;
            case "Stone":
                number.Text = (GlobalVariables.StoneAmount - previousAmount).ToString();
                previousAmount = GlobalVariables.StoneAmount;
                break;
            case "Food":
                number.Text = (GlobalVariables.FoodAmount - previousAmount).ToString();
                previousAmount = GlobalVariables.FoodAmount;
                break;
            case "Gold":
                number.Text = (GlobalVariables.GoldAmount - previousAmount).ToString();
                previousAmount = GlobalVariables.GoldAmount;
                break;
            default:
                break;
        }
    }
}
