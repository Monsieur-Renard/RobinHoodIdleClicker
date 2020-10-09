using Godot;
using System;

public class RessourceCost : Reference
{
    public int woodCost, stoneCost, goldCost, foodCost;

    public RessourceCost(int goldCost, int woodCost, int stoneCost, int foodCost)
    {
        this.goldCost = goldCost;
        this.woodCost = woodCost;
        this.stoneCost = stoneCost;
        this.foodCost = foodCost;
    }
}
