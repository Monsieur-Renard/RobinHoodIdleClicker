using Godot;
using Godot.Collections;
using System;

public class Building : Node2D
{
    [Export]
    private string RessourceType;
    [Export]
    private double BaseValue;
    [Export]
    private int Level;
    [Export]
    public string BuildingName;
    [Export]
    public int NumberOfWorkers;

    private Dictionary<int, RessourceCost> _upgradeCost = new Dictionary<int, RessourceCost>();
    private int MaxLevel = 100;

    Label nameLevel;
    Label goldCostLabel;
    Label woodCostLabel;
    Label stoneCostLabel;
    Label foodCostLabel;
    Button upgradeButton;
    RessourceCost cost;
    VBoxContainer displayContainer;
    NinePatchRect background;
    TextureButton expandButton;
    TextureButton closeButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Initializing ressource cost dictionnary
        PopulateCostDictionnary();

        // Initializing name and level of building
        nameLevel = GetNode<Label>("DisplayContainer/NameLevel");
        nameLevel.Text = BuildingName + " - lvl " + Level;

        // Initializing ressource amount labels
        goldCostLabel = GetNode<Label>("DisplayContainer/RessourcesCost/GoldAmount/Value");
        woodCostLabel = GetNode<Label>("DisplayContainer/RessourcesCost/WoodAmount/Value");
        stoneCostLabel = GetNode<Label>("DisplayContainer/RessourcesCost/StoneAmount/Value");
        foodCostLabel = GetNode<Label>("DisplayContainer/RessourcesCost/FoodAmount/Value");

        // Initializing building upgrade cost
        cost = _upgradeCost[Level];
        goldCostLabel.Text = cost.goldCost.ToString();
        woodCostLabel.Text = cost.woodCost.ToString();
        stoneCostLabel.Text = cost.stoneCost.ToString();
        foodCostLabel.Text = cost.foodCost.ToString();

        // Initialize the upgrade Button
        upgradeButton = GetNode<Button>("VBoxContainer/UpgradeButton");

        displayContainer = GetNode<VBoxContainer>("DisplayContainer");
        background = GetNode<NinePatchRect>("Background");
        expandButton = GetNode<TextureButton>("VBoxContainer/ExpandButton");
        closeButton = GetNode<TextureButton>("VBoxContainer/CloseButton");
    }

    public override void _Process(float delta)
    {
        // Check if there's enough ressources for upgrade to enable button
        if (!EnoughRessourcesForUpgrade())
        {
            upgradeButton.Disabled = true;
        }
        else
        {
            upgradeButton.Disabled = false;
        }
    }

    public void OnTextureButtonPressed()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        double amountGained = BaseValue * Level;

        // Ressource gain
        switch (RessourceType)
        {
            case "Wood":
                globalVariables.WoodAmount += amountGained;
                break;
            case "Stone":
                globalVariables.StoneAmount += amountGained;
                break;
            case "Food":
                globalVariables.FoodAmount += amountGained;
                break;
            case "Gold":
                globalVariables.GoldAmount += amountGained;
                break;
            default:
                break;
        }
    }

    // Upgrade building and substract ressource amount
    public void OnUpgradeButtonPressed()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        Level++;
        globalVariables.GoldAmount -= cost.goldCost;
        globalVariables.WoodAmount -= cost.woodCost;
        globalVariables.StoneAmount -= cost.stoneCost;
        globalVariables.FoodAmount -= cost.foodCost;

        // Change level display
        nameLevel.Text = BuildingName + " - lvl " + Level;

        // Change ressource amount labels
        cost = _upgradeCost[Level];
        goldCostLabel.Text = cost.goldCost.ToString();
        woodCostLabel.Text = cost.woodCost.ToString();
        stoneCostLabel.Text = cost.stoneCost.ToString();
        foodCostLabel.Text = cost.foodCost.ToString();
    }

    // Display building's information
    public void OnExpandButtonPressed()
    {
        background.Visible = true;
        background.SetGlobalPosition(new Vector2(398, 8), false);
        displayContainer.Visible = true;
        displayContainer.SetGlobalPosition(new Vector2(400, 10), false);
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

    public void OnTimerTimeout()
    {
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        // Passive ressource gain each second
        switch (RessourceType)
        {
            case "Wood":
                globalVariables.WoodAmount += NumberOfWorkers * Level;
                break;
            case "Stone":
                globalVariables.StoneAmount += NumberOfWorkers * Level;
                break;
            case "Food":
                globalVariables.FoodAmount += NumberOfWorkers * Level;
                break;
            case "Gold":
                globalVariables.GoldAmount += NumberOfWorkers * Level;
                break;
            default:
                break;
        }
    }


    // Check if player has enough ressources to upgrade building
    public bool EnoughRessourcesForUpgrade()
    {
        bool enoughRessources = false;
        var globalVariables = (GlobalVariables)GetNode("/root/GlobalVariables");

        if (globalVariables.GoldAmount >= cost.goldCost && globalVariables.WoodAmount >= cost.woodCost && globalVariables.StoneAmount >= cost.stoneCost && globalVariables.FoodAmount >= cost.foodCost && Level < MaxLevel)
        {
            enoughRessources = true;
        }

        return enoughRessources;
    }

    public void PopulateCostDictionnary()
    {
        for (int i = 0; i < MaxLevel; i++)
        {
            _upgradeCost.Add(i, new RessourceCost(Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2))));
        }
    }
     
}
