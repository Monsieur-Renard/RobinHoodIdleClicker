using Godot;
using System;

public class MainPage : MarginContainer
{
    [Export]
    PackedScene Builder;
    [Export]
    PackedScene Smoke;

    RigidBody2D builderInstance;
    Node2D smokeInstance;
    Timer builderTimer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        builderTimer = GetNode<Timer>("BuilderTimer");
    }

    // Spawn a builder 
    public void SpawnBuilder()
    {
        // Choose location on Path2D depending of building ARRANGER OFFSET
        var builderSpawnLocation = GetNode<PathFollow2D>("BuilderPath/BuilderSpawnLocation");
        builderSpawnLocation.Offset = 475;

        float direction = builderSpawnLocation.Rotation + Mathf.Pi / 2;
        
        // Create builder instance and add it to scene
        builderInstance = (RigidBody2D)Builder.Instance();
        AddChild(builderInstance);
        builderInstance.Position = builderSpawnLocation.Position;

        // Add some randomness to the direction.
        //direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        //builderInstance.Rotation = direction;

        // Set builder's velocity
        builderInstance.LinearVelocity = new Vector2(150f, 0).Rotated(direction);
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

    public void OnBuilderNeeded()
    {
        SpawnBuilder();
    }

    public void OnBuilderTimeTimeout()
    {
        smokeInstance.QueueFree();
    }
}
