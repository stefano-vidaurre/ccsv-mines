﻿using CCSV.Games;
using CCSV.Mines.Controllers;
using CCSV.Mines.Models;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class OtherView : GameView<BallViewModel>
{
    private readonly IGameWindow _window;

    public OtherView(IGameWindow window, OtherController otherController) : base(otherController)
    {
        _window = window;
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.Black);
    }

    public override void Draw(BallViewModel model)
    {
        long fps = _window.Fps;
        long targetFps = _window.TargetFps;
        long delta = _window.LastDelta;
        long targetDelta = _window.TargetDelta;

        Raylib.DrawText($"FPS: {fps}/{targetFps}", 12, 12, 20, Color.White);
        Raylib.DrawText($"Delta: {delta}/{targetDelta}", 12, 42, 20, Color.White);
        Raylib.DrawCircle(model.PosX, model.PosY, model.Radius, Color.White);
    }
}