using Godot;
using System;
using System.Collections.Generic;

public class MainPage : MarginContainer
{
    [Export]
    PackedScene Builder;
    [Export]
    PackedScene Smoke;

    RigidBody2D builderInstance;
    Node2D smokeInstance;
    Timer builderTimer;
    public bool LoadSavedGame = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        builderTimer = GetNode<Timer>("BuilderTimer");
        if (GlobalVariables.LoadSavedGame)
            LoadGame();
    }

    public void OnTestPressed()
    {
        LoadGame();
    }

    public void LoadGame()
    {     
        var saveGame = new File();
        if (!saveGame.FileExists("user://savegame.save"))
            return;

        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
            saveNode.QueueFree();

        // Load the file line by line and process that dictionary to restore the object it represents
        saveGame.Open("user://savegame.save", File.ModeFlags.Read);

        while (saveGame.GetPosition() < saveGame.GetLen())
        {
            var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
            var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
            Node2D newObject = (Node2D)newObjectScene.Instance();
            GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
            newObject.Position = new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]);
            //newObject.Set("Position", new Vector2((float)nodeData["PosX"]+i, (float)nodeData["PosY"]));

            foreach (KeyValuePair<string, object> entry in nodeData)
            {
                string key = entry.Key.ToString();
                if (key == "Filename" || key == "Parent" || key == "PosX" || key == "PosY")
                    continue;
                newObject.Set(key, entry.Value);
            }
        }

        saveGame.Close();
    }

    // Occurs when builder touches building
    public void OnBuildingHit()
    {
        // Make smoke appears on building
        Vector2 buildingLocation = builderInstance.Position;
        smokeInstance = (Node2D)Smoke.Instance();
        AddChild(smokeInstance);
        smokeInstance.Position = buildingLocation;
        builderTimer.Start();

        //Make builder disapear
        builderInstance.QueueFree();
    }

    // Spawn a builder when signal BuilderNeeded is lit
    public void OnBuilderNeeded(Vector2 position)
    {
        // Choose location on Path2D depending of building
        var builderSpawnLocation = GetNode<PathFollow2D>("BuilderPath/BuilderSpawnLocation");
        builderSpawnLocation.Offset = position.x + 80;

        float direction = builderSpawnLocation.Rotation + Mathf.Pi / 2;

        // Create builder instance and add it to scene
        builderInstance = (RigidBody2D)Builder.Instance();
        AddChild(builderInstance);
        builderInstance.Position = builderSpawnLocation.Position;

        // Set builder's velocity
        builderInstance.LinearVelocity = new Vector2(150f, 0).Rotated(direction);
    }

    public void OnBuilderTimeTimeout()
    {
        smokeInstance.QueueFree();
    }
}
