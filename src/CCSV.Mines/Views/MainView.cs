﻿using CCSV.Games;
using CCSV.Mines.Models;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class MainView : IMainView
{
    private readonly IGameWindow _window;

    public MainView(IGameWindow window)
    {
        _window = window;
    }

    public void Draw(BallViewModel model)
    {
        long fps = _window.Fps;
        long targetFps = _window.TargetFps;
        long delta = _window.LastDelta;
        long targetDelta = _window.TargetDelta;

        Raylib.ClearBackground(Color.White);
        Raylib.DrawText($"FPS: {fps}/{targetFps}", 12, 12, 20, Color.Black);
        Raylib.DrawText($"Delta: {delta}/{targetDelta}", 12, 42, 20, Color.Black);
        Raylib.DrawCircle(model.PosX, model.PosY, model.Radius, Color.Black);
    }
}
