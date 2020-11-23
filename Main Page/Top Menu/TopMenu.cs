using Godot;
using System;

public class TopMenu : MarginContainer
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void OnSaveButtonPressed()
    {
        var saveGame = new File();
        saveGame.Open("user://savegame.save", File.ModeFlags.Write);

        var saveNodes = GetTree().GetNodesInGroup("Persist");
        foreach (Node saveNode in saveNodes)
        {
            GD.Print(saveNode.ToString());
            // Check the node is an instanced scene so it can be instanced again during load
            if (saveNode.Filename.Empty())
            {
                GD.Print(String.Format("persistent node '{0}' is not an instanced scene, skipped", saveNode.Name));
                continue;
            }

            // Check the node has a save function.
            if (!saveNode.HasMethod("Save"))
            {
                GD.Print(String.Format("persistent node '{0}' is missing a Save() function, skipped", saveNode.Name));
                continue;
            }

            // Call the node's save function
            var nodeData = saveNode.Call("Save");

            // Store the save dictionnary as a new line in the save file
            saveGame.StoreLine(JSON.Print(nodeData));
        }

        var globalNodeData = GlobalVariables.Save();
        saveGame.StoreLine(JSON.Print(globalNodeData));


        saveGame.Close();
    }   
}
