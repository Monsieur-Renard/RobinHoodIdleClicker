using Godot;
using Godot.Collections;
using System;

public class Camp : Node2D
{
    private int TotalNumberOfWorkers, MaxWorkers;
    private Dictionary<int, RessourceCost> _upgradeCost = new Dictionary<int, RessourceCost>();
    RessourceCost cost;
    Label ForestLabel, MineLabel, FieldLabel, VillageLabel, UnassignedLabel, goldCostLabel, foodCostLabel;
    TextureButton expandButton, closeButton;
    VBoxContainer displayContainer;
    NinePatchRect background;
    Button ForestButton, MineButton, FieldButton, VillageButton, buyButton;
    Building forestBuldingNode, mineBuldingNode, fieldBuldingNode, villageBuldingNode;

    [Export]
    public int UnassignedWorkers;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Initializing ressource cost dictionnary
        MaxWorkers = 100;
        TotalNumberOfWorkers = 0;
        PopulateCostDictionnary();

        // Get building nodes attributes
        forestBuldingNode = GetParent().GetNode<Building>("BuildingWood");
        mineBuldingNode = GetParent().GetNode<Building>("BuildingStone");
        fieldBuldingNode = GetParent().GetNode<Building>("BuildingFood");
        villageBuldingNode = GetParent().GetNode<Building>("BuildingGold");

        // Initializing Name and number of workers of labels
        ForestLabel = GetNode<Label>("Assignement/Forest/NumberLabel");
        MineLabel = GetNode<Label>("Assignement/Mine/NumberLabel");
        FieldLabel = GetNode<Label>("Assignement/Field/NumberLabel");
        VillageLabel = GetNode<Label>("Assignement/Village/NumberLabel");
        UnassignedLabel = GetNode<Label>("Assignement/VBoxContainer/LabelQuantity");

        ForestLabel.Text = forestBuldingNode.NumberOfWorkers.ToString();
        MineLabel.Text = mineBuldingNode.NumberOfWorkers.ToString();
        FieldLabel.Text = fieldBuldingNode.NumberOfWorkers.ToString();
        VillageLabel.Text = villageBuldingNode.NumberOfWorkers.ToString();

        // Initializing cost of new worker
        goldCostLabel = GetNode<Label>("Assignement/RessourceCost/GoldAmount/Value");
        foodCostLabel = GetNode<Label>("Assignement/RessourceCost/FoodAmount/Value");
        cost = _upgradeCost[TotalNumberOfWorkers];
        goldCostLabel.Text = cost.goldCost.ToString();
        foodCostLabel.Text = cost.goldCost.ToString();

        // Initialize other nodes
        expandButton = GetNode<TextureButton>("VBoxContainer/ExpandButton");
        closeButton = GetNode<TextureButton>("VBoxContainer/CloseButton");
        buyButton = GetNode<Button>("Assignement/CenterContainer/BuyButton");
        ForestButton = GetNode<Button>("Assignement/Forest/Button");
        MineButton = GetNode<Button>("Assignement/Mine/Button");
        FieldButton = GetNode<Button>("Assignement/Field/Button");
        VillageButton = GetNode<Button>("Assignement/Village/Button");
        background = GetNode<NinePatchRect>("Background");
        displayContainer = GetNode<VBoxContainer>("Assignement");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Check if there's enough ressources to buy a merryman to enable button
        if (!EnoughRessourcesForMerrymen())
        {
            buyButton.Disabled = true;
        }
        else
        {
            buyButton.Disabled = false;
        }

        // Enable assign buttons if there's an unassigned worker
        if (UnassignedWorkers > 0)
        {
            MineButton.Disabled = false;
            FieldButton.Disabled = false;
            VillageButton.Disabled = false;
            ForestButton.Disabled = false;
        }
        else
        {
            MineButton.Disabled = true;
            FieldButton.Disabled = true;
            VillageButton.Disabled = true;
            ForestButton.Disabled = true;
        }
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

    // Add a merryman to the UnassignedWorkers' value
    public void OnBuyButtonPressed()
    {
        TotalNumberOfWorkers++;
        UnassignedWorkers++;
        GlobalVariables.GoldAmount -= cost.goldCost;
        GlobalVariables.FoodAmount -= cost.foodCost;

        // Change number of assigned merrymen display
        UnassignedLabel.Text = UnassignedWorkers.ToString();

        // Change ressource amount labels
        cost = _upgradeCost[TotalNumberOfWorkers];
        goldCostLabel.Text = cost.goldCost.ToString();
        foodCostLabel.Text = cost.foodCost.ToString();
    }

    public void OnForestButtonPressed()
    {
        UnassignedWorkers--;
        UnassignedLabel.Text = UnassignedWorkers.ToString();
        forestBuldingNode.NumberOfWorkers++;
        ForestLabel.Text = forestBuldingNode.NumberOfWorkers.ToString();
    }

    public void OnMineButtonPressed()
    {
        UnassignedWorkers--;
        UnassignedLabel.Text = UnassignedWorkers.ToString();
        mineBuldingNode.NumberOfWorkers++;
        MineLabel.Text = mineBuldingNode.NumberOfWorkers.ToString();
    }

    public void OnFieldButtonPressed()
    {
        UnassignedWorkers--;
        UnassignedLabel.Text = UnassignedWorkers.ToString();
        fieldBuldingNode.NumberOfWorkers++;
        FieldLabel.Text = fieldBuldingNode.NumberOfWorkers.ToString();
    }

    public void OnVillageButtonPressed()
    {
        UnassignedWorkers--;
        UnassignedLabel.Text = UnassignedWorkers.ToString();
        villageBuldingNode.NumberOfWorkers++;
        VillageLabel.Text = villageBuldingNode.NumberOfWorkers.ToString();
    }

    public bool EnoughRessourcesForMerrymen()
    {
        bool enoughRessources = false;

        if (GlobalVariables.GoldAmount >= cost.goldCost && GlobalVariables.FoodAmount >= cost.foodCost)
        {
            enoughRessources = true;
        }

        return enoughRessources;
    }

    // Fill up cost dictionnary
    private void PopulateCostDictionnary()
    {
        for (int i = 0; i < MaxWorkers; i++)
        {
            _upgradeCost.Add(i, new RessourceCost(Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2))));
        }
    }
}
