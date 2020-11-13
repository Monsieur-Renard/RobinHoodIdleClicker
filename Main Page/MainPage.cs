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
        // Choose location on Path2D depending of building ARRANGER OFFSET
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
