using Godot;
using Godot.Collections;
using System;

public class GlobalVariables : Node
{
    public double WoodAmount;
    public double StoneAmount;
    public double FoodAmount;
    public double GoldAmount;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        WoodAmount = 0;
        StoneAmount = 0;
        FoodAmount = 0;
        GoldAmount = 0;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
