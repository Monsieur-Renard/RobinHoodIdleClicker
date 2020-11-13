using Godot;
using System;
using System.Collections.Generic;

public class Tool : VBoxContainer
{
    [Export]
    private string RessourceType;
    private Dictionary<int, RessourceCost> _upgradeCost = new Dictionary<int, RessourceCost>();
    private int MaxLevel = 100;
    RessourceCost cost;
    public int Level;
    Button upgradeButton;

    Label levelLabel, goldCostLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Set tool level
        switch (RessourceType)
        {
            case "Wood":
                Level = GlobalVariables.AxeLevel;
                break;
            case "Stone":
                Level = GlobalVariables.PickaxeLevel;
                break;
            case "Food":
                Level = GlobalVariables.PitchforkLevel;
                break;
            default:
                break;
        }

        // Initialize ressource cost dictionnary
        PopulateCostDictionnary();

        // Initialize level of building
        levelLabel = GetNode<Label>("Level");
        levelLabel.Text = "Level " + Level;

        // Initialize ressource amount label
        goldCostLabel = GetNode<Label>("RessourceCost/Cost");
        cost = _upgradeCost[Level];
        goldCostLabel.Text = cost.goldCost.ToString();

        // Initialize remaining nodes
        upgradeButton = GetNode<Button>("CenterContainer/UpgradeButton");
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

    public void OnUpgradeButtonPressed()
    {
        Level++;
        GlobalVariables.GoldAmount -= cost.goldCost;

        // Change level display
        levelLabel.Text = "Level " + Level;

        // Change ressource amount labels
        cost = _upgradeCost[Level];
        goldCostLabel.Text = cost.goldCost.ToString();

        switch (RessourceType)
        {
            case "Wood":
                GlobalVariables.AxeLevel++;
                break;
            case "Stone":
                GlobalVariables.PickaxeLevel++;
                break;
            case "Food":
                GlobalVariables.PitchforkLevel++;
                break;
            default:
                break;
        }
    }

    // Check if player has enough ressources to upgrade building
    private  bool EnoughRessourcesForUpgrade()
    {
        bool enoughRessources = false;

        if (GlobalVariables.GoldAmount >= cost.goldCost && Level < MaxLevel)
        {
            enoughRessources = true;
        }

        return enoughRessources;
    }

    // Fill up cost dictionnary
    public void PopulateCostDictionnary()
    {
        for (int i = 0; i < MaxLevel; i++)
        {
            _upgradeCost.Add(i, new RessourceCost(Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2))));
        }
    }
}
