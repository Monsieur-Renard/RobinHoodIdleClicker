using Godot;
using Godot.Collections;
using System;

public class GlobalVariables : Node
{
    public static double WoodAmount;
    public static double StoneAmount;
    public static double FoodAmount;
    public static double GoldAmount;
    public static int PitchforkLevel;
    public static int PickaxeLevel;
    public static int AxeLevel;
    public static bool LoadSavedGame;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (!LoadSavedGame)
        {
            WoodAmount = 0;
            StoneAmount = 0;
            FoodAmount = 0;
            GoldAmount = 0;
            PitchforkLevel = 1;
            PickaxeLevel = 1;
            AxeLevel = 1;
        }
    }

    // Serializes datas for save
    public static Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
        {
            { "Filename", "GlobalVariables" },
            { "WoodAmount", WoodAmount },
            { "StoneAmount", StoneAmount },
            { "FoodAmount", FoodAmount },
            { "GoldAmount", GoldAmount },
            { "PitchforkLevel", PitchforkLevel },
            { "PickaxeLevel", PickaxeLevel },
            { "AxeLevel", AxeLevel }
        };
    }
}
