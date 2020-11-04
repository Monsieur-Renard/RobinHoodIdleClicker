using Godot;
using System;

public class BuildingForge : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void OnGoToForgePressed()
    {
        var forge = GetNode<MarginContainer>("../../ForgeWindow");
        forge.Visible = true;
    }
}
