namespace CCSV.Games;
public abstract class GameController : IGameController
{
    private readonly IGameView _gameView;
    private readonly GameViewHandler _gameViewHandler;

    protected GameController(IGameView gameView)
    {
        _gameViewHandler = new GameViewHandler(gameView.Window, gameView);
        _gameView = gameView;
    }

    public void Draw()
    {
        _gameViewHandler.Draw();
    }

    public Task Update()
    {
        return Task.Run(() =>
        {
            while (true)
            {
                ;
            }
        });
    }
}

public abstract class GameController<Vmodel> : IGameController<Vmodel> where Vmodel : GameViewModel
{
    private readonly IGameView<Vmodel> _gameView;
    private readonly GameViewHandler<Vmodel> _gameViewHandler;

    protected GameController(IGameView<Vmodel> gameView)
    {
        _gameViewHandler = new GameViewHandler<Vmodel>(gameView.Window, gameView);
        _gameView = gameView;
    }
    public abstract Vmodel GetViewModel();

    public void Draw()
    {
        Vmodel model = GetViewModel();
        _gameViewHandler.Draw(model);
    }

    public Task Update()
    {
        return Task.Run(() =>
        {
            while (true)
            {
                ;
            }
        });
    }
}
