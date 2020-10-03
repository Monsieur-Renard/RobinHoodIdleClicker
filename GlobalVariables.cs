using Godot;
using System;

public class GlobalVariables : Node
{
    public float WoodAmount;
    public float StoneAmount;
    public float FoodAmount;
    public float GoldAmount;

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
