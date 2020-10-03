using Godot;
using System;

public class Building : Node2D
{
    [Signal]
    public delegate void WoodChange();

    [Signal]
    public delegate void StoneChange();

    [Signal]
    public delegate void FoodChange();

    [Signal]
    public delegate void GoldChange();

    [Export]
    private string RessourceType;
    [Export]
    private float BaseValue;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void OnTextureButtonPressed()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        switch (RessourceType)
        {
            case "wood":
                globalVariables.WoodAmount += BaseValue;
                EmitSignal("WoodChange");
                break;
            case "stone":
                globalVariables.StoneAmount += BaseValue;
                EmitSignal("StoneChange");
                break;
            case "food":
                globalVariables.FoodAmount += BaseValue;
                EmitSignal("FoodChange");
                break;
            case "gold":
                globalVariables.GoldAmount += BaseValue;
                GD.Print(globalVariables.GoldAmount);
                EmitSignal("GoldChange");
                break;
            default:
                break;
        }
    
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
