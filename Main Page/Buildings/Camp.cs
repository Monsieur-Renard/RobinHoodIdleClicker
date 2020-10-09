using Godot;
using Godot.Collections;
using System;

public class Camp : Node2D
{
    Label ForestLabel, MineLabel, FieldLabel;
    private int TotalNumberOfWorkers;
    private Dictionary<int, RessourceCost> _upgradeCost = new Dictionary<int, RessourceCost>();



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Get building nodes attributes
        var forestBuldingNode = GetParent().GetNode<Building>("BuildingWood");
        var mineBuldingNode = GetParent().GetNode<Building>("BuildingStone");
        var fieldBuldingNode = GetParent().GetNode<Building>("BuildingFood");

        // Initializing Name and number of workers of labels
        ForestLabel = GetNode<Label>("Assignement/Forest/ForestLabel");
        MineLabel = GetNode<Label>("Assignement/Mine/MineLabel");
        FieldLabel = GetNode<Label>("Assignement/Field/FieldLabel");

        ForestLabel.Text = forestBuldingNode.BuildingName + " Qty: " + forestBuldingNode.NumberOfWorkers;
        MineLabel.Text = mineBuldingNode.BuildingName + " Qty: " + mineBuldingNode.NumberOfWorkers;
        FieldLabel.Text = fieldBuldingNode.BuildingName + " Qty: " + fieldBuldingNode.NumberOfWorkers;

        // Initializing cost of new worker
        TotalNumberOfWorkers = forestBuldingNode.NumberOfWorkers + mineBuldingNode.NumberOfWorkers + fieldBuldingNode.NumberOfWorkers;

    }

    public void OnForestButtonPressed()
    {

    }

    public void OnMineButtonPressed()
    {

    }

    public void OnFieldButtonPressed()
    {

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
