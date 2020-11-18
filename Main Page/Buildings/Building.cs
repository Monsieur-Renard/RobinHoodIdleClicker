using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;

public class Building : Node2D
{
    [Export]
    public string RessourceType;
    [Export]
    public double BaseValue;
    [Export]
    public int Level;
    [Export]
    public string BuildingName;
    [Export]
    public int NumberOfWorkers;

    [Signal]
    public delegate void Hit();
    [Signal]
    public delegate void BuilderNeeded(Vector2 position);

    private Dictionary<int, RessourceCost> _upgradeCost = new Dictionary<int, RessourceCost>();
    private int MaxLevel = 100;

    Label nameLevel, goldCostLabel, woodCostLabel, stoneCostLabel, foodCostLabel;
    Button upgradeButton;
    RessourceCost cost;
    VBoxContainer displayContainer;
    NinePatchRect background;
    TextureButton expandButton, closeButton, imageButton;
    AudioStreamPlayer fieldSound, mineSound, forestSound;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Initializing ressource cost dictionnary
        PopulateCostDictionnary();

        // Initializing name and level of building
        nameLevel = GetNode<Label>("DisplayContainer/NameLevel");
        //nameLevel.Text = BuildingName + " - lvl " + Level;

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

        // Initialize remaining nodes
        upgradeButton = GetNode<Button>("VBoxContainer/UpgradeButton");
        displayContainer = GetNode<VBoxContainer>("DisplayContainer");
        background = GetNode<NinePatchRect>("Background");
        expandButton = GetNode<TextureButton>("VBoxContainer/ExpandButton");
        closeButton = GetNode<TextureButton>("VBoxContainer/CloseButton");
        imageButton = GetNode<TextureButton>("VBoxContainer/ImageButton");

        // Initialize sounds
        fieldSound = GetNode<AudioStreamPlayer>("FieldSound");
        mineSound = GetNode<AudioStreamPlayer>("MineSound");
        forestSound = GetNode<AudioStreamPlayer>("ForestSound");

        // Add to group to save
        AddToGroup("Persist");

        if (GlobalVariables.LoadSavedGame)
            ReadyFromContinue();
        else
            nameLevel.Text = BuildingName + " - lvl " + Level;
    }

    public void ReadyFromContinue()
    {
        // Initializing building upgrade cost
        cost = _upgradeCost[Level];
        goldCostLabel.Text = cost.goldCost.ToString();
        woodCostLabel.Text = cost.woodCost.ToString();
        stoneCostLabel.Text = cost.stoneCost.ToString();
        foodCostLabel.Text = cost.foodCost.ToString();
        nameLevel.Text = BuildingName + " - lvl " + Level;
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

        //Check if there's enough ressource to get enable gold button
        if (RessourceType == "Gold")
        {
            if (!EnoughRessourcesForGold())
            {
                imageButton.Disabled = true;
            }
            else
            {
                imageButton.Disabled = false;
            }
        }
    }

    public void OnTextureButtonPressed()
    {
        double amountGained = RessourceGained();
        forestSound.Stop();
        mineSound.Stop();
        fieldSound.Stop();

        // Ressource gain
        switch (RessourceType)
        {
            case "Wood":
                GlobalVariables.WoodAmount += amountGained;
                forestSound.Play();
                break;
            case "Stone":
                GlobalVariables.StoneAmount += amountGained;
                mineSound.Play();
                break;
            case "Food":
                GlobalVariables.FoodAmount += amountGained;
                fieldSound.Play();
                break;
            case "Gold":
                GlobalVariables.GoldAmount += amountGained;
                GlobalVariables.StoneAmount -= amountGained / 2;
                GlobalVariables.WoodAmount -= amountGained / 2;
                break;
            default:
                break;
        }
    }

    // Upgrade building and substract ressource amount
    public void OnUpgradeButtonPressed()
    {
        Level++;
        if (Level % 5 == 0)
        {
            EmitSignal("BuilderNeeded", imageButton.RectGlobalPosition);
        }
        GlobalVariables.GoldAmount -= cost.goldCost;
        GlobalVariables.WoodAmount -= cost.woodCost;
        GlobalVariables.StoneAmount -= cost.stoneCost;
        GlobalVariables.FoodAmount -= cost.foodCost;

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

    // When builder collides with building
    public void OnArea2DBodyEntered(PhysicsBody2D body)
    {
        EmitSignal("Hit");        
    }

    public void OnTimerTimeout()
    {
        // Passive ressource gain each second
        switch (RessourceType)
        {
            case "Wood":
                GlobalVariables.WoodAmount += NumberOfWorkers * Level;
                break;
            case "Stone":
                GlobalVariables.StoneAmount += NumberOfWorkers * Level;
                break;
            case "Food":
                GlobalVariables.FoodAmount += NumberOfWorkers * Level;
                break;
            case "Gold":
                GlobalVariables.GoldAmount += NumberOfWorkers * Level;
                break;
            default:
                break;
        }
    }

    // Serializes datas for save
    public Godot.Collections.Dictionary<string, object> Save()
    {
        return new Godot.Collections.Dictionary<string, object>()
        {
            { "Filename", Filename },
            { "Parent", GetParent().GetPath() },
            { "PosX", Position.x },
            { "PosY", Position.y },
            { "BuildingName", BuildingName },
            { "Level", Level },
            { "NumberOfWorkers", NumberOfWorkers }
        };
    }

    // Check if player has enough ressources to upgrade building
    public bool EnoughRessourcesForUpgrade()
    {
        bool enoughRessources = false;

        if (GlobalVariables.GoldAmount >= cost.goldCost && GlobalVariables.WoodAmount >= cost.woodCost && GlobalVariables.StoneAmount >= cost.stoneCost && GlobalVariables.FoodAmount >= cost.foodCost && Level < MaxLevel)
        {
            enoughRessources = true;
        }

        return enoughRessources;
    }

    // Check if player has enough ressources to buy gold
    public bool EnoughRessourcesForGold()
    {
        double amountGained = RessourceGained();
        bool enoughRessources = false;

        if ((GlobalVariables.WoodAmount / 2 >= amountGained) && (GlobalVariables.StoneAmount / 2 >= amountGained))
        {
            enoughRessources = true;
        }

        return enoughRessources;
    }

    // Calculate ressource gain
    public double RessourceGained()
    {
        int toolLevel = 1; 

        switch (RessourceType)
        {
            case "Wood":
                toolLevel = GlobalVariables.AxeLevel;
                break;
            case "Stone":
                toolLevel = GlobalVariables.PickaxeLevel;
                break;
            case "Food":
                toolLevel = GlobalVariables.PitchforkLevel;
                break;
            case "Gold":
                toolLevel = 1;
                break;
            default:
                break;
        }

        return BaseValue * Level * toolLevel;       
    }

    // Fill up cost dictionnary
    private void PopulateCostDictionnary()
    {
        for (int i = 0; i < MaxLevel; i++)
        {
            _upgradeCost.Add(i, new RessourceCost(Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2)), Convert.ToInt32(Math.Pow(i, 2))));
        }
    }   
}
