using Godot;
using System;

public class Options : MarginContainer
{
    private bool SoundMuted, MusicMuted;
    CheckBox SoundMuteCheckbox, MusicMuteCheckBox;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SoundMuteCheckbox = GetNode<CheckBox>("VBoxContainer/SoundContainer/MuteCheckBox");
        MusicMuteCheckBox = GetNode<CheckBox>("VBoxContainer/MusicContainer/MuteCheckBox");
        
        // Check if sound is muted
        if (AudioServer.IsBusMute(AudioServer.GetBusIndex("Sound")))
        {
            SoundMuteCheckbox.Pressed = true;
            SoundMuted = true;
        }
        else
        {
            SoundMuteCheckbox.Pressed = false;
            SoundMuted = false;
        }

        // Check if music is muted
        if (AudioServer.IsBusMute(AudioServer.GetBusIndex("Music")))
        {
            MusicMuteCheckBox.Pressed = true;
            MusicMuted = true;
        }
        else
        {
            MusicMuteCheckBox.Pressed = false;
            MusicMuted = false;
        }
    }

    // Goes back to TitlePage scene without saving
    public void OnCancelPressed()
    {
        GetTree().ChangeScene("res://Title Page/TitlePage.tscn");
    }

    // Mute sound
    public void OnSoundMuteCheckBoxPressed()
    {

        if (SoundMuteCheckbox.Pressed == true)
            SoundMuted = true;
        else
            SoundMuted = false;
    }

    // Mute music
    public void OnMusicMuteCheckBoxPressed()
    {
        if (MusicMuteCheckBox.Pressed == true)
            MusicMuted = true;
        else
            MusicMuted = false;
    }

    // Saves options and goes back to TitlePage scene
    public void OnSavePressed()
    {
        if (SoundMuted)
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Sound"), true);
        else
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Sound"), false);

        if (MusicMuted)
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Music"), true);
        else
            AudioServer.SetBusMute(AudioServer.GetBusIndex("Music"), false);

        GetTree().ChangeScene("res://Title Page/TitlePage.tscn");
    }
}
