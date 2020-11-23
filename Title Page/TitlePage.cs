using Godot;
using System;
using System.Collections.Generic;

public class TitlePage : MarginContainer
{
    Sprite logo;
    int h, w;
    float i = 0.1f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        logo = GetNode<Sprite>("Logo");
        h = logo.Texture.GetHeight();
        w = logo.Texture.GetWidth();
        logo.Scale = new Vector2(0.1f, 0.1f);
        logo.Position = new Vector2(GetViewport().Size.x / 2 - w * i/2, GetViewport().Size.y / 7 * 2 - h*i/2);
        var timer = GetNode<Timer>("Timer");
        timer.Start();
    }

    // Called when timer's times out
    public void OnTimerTimeout()
    {
        var timer = GetNode<Timer>("Timer");

        i += 0.04f;
        logo.Scale = new Vector2(0.1f + i, 0.1f + i);
        logo.Position = new Vector2(GetViewport().Size.x / 2 - w * i / 2, GetViewport().Size.y / 7 * 2 - h * i / 2);
        if (i >= 1f)
        {
            logo.Position = new Vector2(GetViewport().Size.x / 2 - w * i / 2, GetViewport().Size.y / 7 * 2 - h * i / 2);
            timer.Stop();
        }
        
    }

    // Go to Main Game scene
    public void OnNewGamePressed()
    {
        GetTree().ChangeScene("res://Main Page/MainPage.tscn");
    }

    // Loads the game
    public void OnContinuePressed()
    {
        var nextLevelResource = ResourceLoader.Load("res://Main Page/MainPage.tscn") as PackedScene;
        MainPage nextLevel = (MainPage)nextLevelResource.Instance();
        GlobalVariables.LoadSavedGame = true;
        GetTree().Root.CallDeferred("add_child", nextLevel);
        QueueFree();            
    }

    // Exits the game
    public void OnQuitPressed()
    {
        GetTree().Quit();
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
            var newObject = (Node)newObjectScene.Instance();
            GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
            newObject.Set("Position", new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]));

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
}
