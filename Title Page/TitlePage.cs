using Godot;
using System;

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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
    }

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

    public void OnNewGamePressed()
    {
        GetTree().ChangeScene("res://Main Page/MainPage.tscn");
    }
}
