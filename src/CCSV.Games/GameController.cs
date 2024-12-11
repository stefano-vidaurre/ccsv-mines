namespace CCSV.Games;
public abstract class GameController : IGameController
{
    private readonly IGameView _gameView;

    protected GameController(IGameView gameView)
    {
        _gameView = gameView;
    }

    public void Draw()
    {
        _gameView.Draw();
    }
}

public abstract class GameController<Vmodel> : IGameController<Vmodel> where Vmodel : GameViewModel
{
    private readonly IGameView<Vmodel> _gameView;

    protected GameController(IGameView<Vmodel> gameView)
    {
        _gameView = gameView;
    }
    public abstract Vmodel GetViewModel();

    public void Draw()
    {
        Vmodel model = GetViewModel();
        _gameView.Draw();
        _gameView.Draw(model);
    }
}
