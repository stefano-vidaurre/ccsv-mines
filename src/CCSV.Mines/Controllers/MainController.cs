using CCSV.Games;
using CCSV.Mines.Domain.Models;
using CCSV.Mines.Models;
using CCSV.Mines.Views;
using Raylib_cs;

namespace CCSV.Mines.Controllers;

public class MainController : GameController<BallViewModel>, IMainController
{
    private readonly IMainView _mainView;
    private readonly Ball _ball;

    public MainController(IMainView mainView) : base(mainView)
    {
        _mainView = mainView;
        _ball = Ball.Create(Guid.NewGuid(), 20, 80);
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

    [KeyboardPressed((int) KeyboardKey.Left)]
    public void OnLeftKeyPressed(long delta, bool firstUpdate)
    {
        if (firstUpdate)
        {
            _ball.MoveX(-1);
        }
    }

    [KeyboardPressed((int)KeyboardKey.Right)]
    public void OnRightKeyPressed(long delta, bool firstUpdate)
    {
        if (firstUpdate)
        {
            _ball.MoveX(1);
        }
    }
}
