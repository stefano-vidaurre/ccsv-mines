using CCSV.Games;
using CCSV.Games.Raylib;
using CCSV.Mines.Domain.Models;
using CCSV.Mines.Models;
using CCSV.Mines.Views;
using Raylib_cs;

namespace CCSV.Mines.Controllers;

public class MainController : GameController<BallViewModel>, IMainController
{
    private readonly Ball _ball;
    private readonly IMainView _mainView;

    public MainController(IMainView mainView) : base(mainView)
    {
        _ball = Ball.Create(Guid.NewGuid(), 20, 80);
        _mainView = mainView;
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
    public void OnLeftKeyPressed(long delta)
    {
        int move = (int) (-1 * 240 * delta / TimeSpan.TicksPerSecond);
        _ball.MoveX(move);
    }

    [KeyboardDown(KeyboardKey.Right)]
    public void OnRightKeyPressed(long delta)
    {
        int move = (int) (1 * 240 * delta / TimeSpan.TicksPerSecond);
        _ball.MoveX(move);
    }

    [KeyboardPressed(KeyboardKey.Up)]
    public void OnUpKeyPressed()
    {
        if(_mainView.Window.TargetFps >= 240)
        {
            return;
        }

        _mainView.Window.SetTargetFps(_mainView.Window.TargetFps * 2);
    }

    [KeyboardPressed(KeyboardKey.Down)]
    public void OnDownKeyPressed()
    {
        if (_mainView.Window.TargetFps <= 30)
        {
            return;
        }

        _mainView.Window.SetTargetFps(_mainView.Window.TargetFps / 2);
    }
}
