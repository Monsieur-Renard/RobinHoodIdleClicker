using Godot;
using Godot.Collections;
using System;

public class Camp : Node2D
{
    Label ForestLabel, MineLabel, FieldLabel;
    private int TotalNumberOfWorkers;
    private Dictionary<int, RessourceCost> _upgradeCost = new Dictionary<int, RessourceCost>();
    TextureButton expandButton;
    TextureButton closeButton;
    VBoxContainer displayContainer;
    NinePatchRect background;

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

        // Initialize other nodes
        expandButton = GetNode<TextureButton>("VBoxContainer/ExpandButton");
        closeButton = GetNode<TextureButton>("VBoxContainer/CloseButton");
        background = GetNode<NinePatchRect>("Background");
        displayContainer = GetNode<VBoxContainer>("Assignement");
    }

    // Display building's information
    public void OnExpandButtonPressed()
    {
        background.Visible = true;
        background.SetGlobalPosition(new Vector2(298, 8), false);
        displayContainer.Visible = true;
        displayContainer.SetGlobalPosition(new Vector2(300, 10), false);
        closeButton.Visible = true;
        expandButton.Visible = false;
    }

    // Hide building's information
    public void OnCloseButtonPressed()
    {
        background.Visible = false;
        displayContainer.Visible = false;
        closeButton.Visible = false;
        expandButton.Visible = true;
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
