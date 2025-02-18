﻿using CCSV.Games;
using CCSV.Games.Raylib;
using CCSV.Mines.Domain.Models;
using CCSV.Mines.Models;
using CCSV.Mines.Views;
using Raylib_cs;

namespace CCSV.Mines.Controllers;

public class MainController : IGameController<BallViewModel>
{
    private readonly Ball _ball;
    private readonly IGameWindow _window;
    private long _updateCounterFps;
    private long _updateCounterFree;

    public MainController(IGameWindow window)
    {
        _updateCounterFps = 0;
        _updateCounterFree = 0;
        _ball = Ball.Create(Guid.NewGuid(), 20, 80);
        _window = window;
    }

    public BallViewModel GetFirstLayerViewModel()
    {
        return new BallViewModel()
        {
            PosX = _ball.PosX,
            PosY = _ball.PosY,
            Radius = _ball.Radius,
            UpdateCounterFps = _updateCounterFps,
            UpdateCounterFree = _updateCounterFree
        };
    }

    [UpdateProcess]
    public void ProcessByFrame()
    {
        _updateCounterFps++;
    }

    [UpdateProcess]
    [DesyncEvent]
    public void ProcessByFree()
    {
        _updateCounterFree++;
    }

    [KeyboardDown(KeyboardKey.Left)]
    public void OnLeftKeyDown(long delta)
    {
        int move = (int)(-1 * 240 * delta / TimeSpan.TicksPerSecond);

        if (_ball.PosX + move < _ball.Radius)
        {
            return;
        }

        _ball.MoveX(move);
    }

    [KeyboardDown(KeyboardKey.Right)]
    public void OnRightKeyDown(long delta)
    {
        int move = (int)(1 * 240 * delta / TimeSpan.TicksPerSecond);

        if (_ball.PosX + move > _window.Width - _ball.Radius)
        {
            return;
        }

        _ball.MoveX(move);
    }

    [KeyboardPressed(KeyboardKey.Up)]
    public void OnUpKeyPressed()
    {
        if (_window.TargetFps >= 240)
        {
            return;
        }

        _window.SetTargetFps(_window.TargetFps * 2);
    }

    [KeyboardPressed(KeyboardKey.Down)]
    public void OnDownKeyPressed()
    {
        if (_window.TargetFps <= 30)
        {
            return;
        }

        _window.SetTargetFps(_window.TargetFps / 2);
    }

    [KeyboardPressed(KeyboardKey.Z)]
    public void OnZKeyPressed()
    {
        if(_window.CurrentViewType == typeof(MainView))
        {
            _window.NextView<OtherView>();
        }
        else
        {
            _window.NextView<MainView>();
        }
    }

    [KeyboardPressed(KeyboardKey.Escape)]
    public void OnEscapeKeyPressed()
    {
        _window.Close();
    }
}
