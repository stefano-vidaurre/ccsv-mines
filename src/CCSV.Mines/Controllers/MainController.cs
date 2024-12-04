using CCSV.Games;
using CCSV.Games.Raylib;
using CCSV.Mines.Domain.Models;
using CCSV.Mines.Models;
using CCSV.Mines.Views;
using Raylib_cs;

namespace CCSV.Mines.Controllers;

public class MainController : GameController<BallViewModel>
{
    private readonly Ball _ball;
    private readonly IGameWindow _window;

    public MainController(IMainView mainView, IGameWindow window) : base(mainView)
    {
        _ball = Ball.Create(Guid.NewGuid(), 20, 80);
        _window = window;
    }

    public override BallViewModel GetViewModel()
    {
        return new BallViewModel()
        {
            PosX = _ball.PosX,
            PosY = _ball.PosY,
            Radius = _ball.Radius,
        };
    }

    [KeyboardDown(KeyboardKey.Left)]
    public void OnLeftKeyDown(long delta)
    {
        int move = (int) (-1 * 240 * delta / TimeSpan.TicksPerSecond);

        if(_ball.PosX + move < _ball.Radius)
        {
            return;
        }

        _ball.MoveX(move);
    }

    [KeyboardDown(KeyboardKey.Right)]
    public void OnRightKeyDown(long delta)
    {
        int move = (int) (1 * 240 * delta / TimeSpan.TicksPerSecond);

        if (_ball.PosX + move > _window.Width - _ball.Radius)
        {
            return;
        }

        _ball.MoveX(move);
    }

    [KeyboardPressed(KeyboardKey.Up)]
    public void OnUpKeyPressed()
    {
        if(_window.TargetFps >= 240)
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

    [KeyboardPressed(KeyboardKey.Escape)]
    public void OnEscapeKeyPressed()
    {
        _window.Close();
    }
}
