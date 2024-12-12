namespace CCSV.Games;
public abstract class GameView : IGameView
{
    public abstract void Draw();

    IGameController? IGameView.__Controller => null;

    void IGameView.__Draw()
    {
        Draw();
    }
}

public abstract class GameView<TFirstModel> : IGameView<TFirstModel> where TFirstModel : GameViewModel
{
    private readonly IGameController<TFirstModel> _gameController;

    IGameController? IGameView.__Controller => _gameController;

    protected GameView(IGameController<TFirstModel> gameController)
    {
        _gameController = gameController;
    }

    public abstract void Draw();

    public abstract void Draw(TFirstModel model);

    void IGameView.__Draw()
    {
        Draw();
        Draw(_gameController.GetFirstLayerViewModel());
    }
}

public abstract class GameView<TFirstModel, TSecondModel> : IGameView<TFirstModel, TSecondModel>
    where TFirstModel : GameViewModel
    where TSecondModel : GameViewModel
{
    private readonly IGameController<TFirstModel, TSecondModel> _gameController;

    IGameController? IGameView.__Controller => _gameController;

    protected GameView(IGameController<TFirstModel, TSecondModel> gameController)
    {
        _gameController = gameController;
    }

    public abstract void Draw();

    public abstract void Draw(TFirstModel model);

    public abstract void Draw(TSecondModel model);

    void IGameView.__Draw()
    {
        Draw();
        Draw(_gameController.GetFirstLayerViewModel());
        Draw(_gameController.GetSecondLayerViewModel());
    }
}

public abstract class GameView<TFirstModel, TSecondModel, TThirdModel> : IGameView<TFirstModel, TSecondModel, TThirdModel>
    where TFirstModel : GameViewModel
    where TSecondModel : GameViewModel
    where TThirdModel : GameViewModel
{
    private readonly IGameController<TFirstModel, TSecondModel, TThirdModel> _gameController;

    IGameController? IGameView.__Controller => _gameController;

    protected GameView(IGameController<TFirstModel, TSecondModel, TThirdModel> gameController)
    {
        _gameController = gameController;
    }

    public abstract void Draw();

    public abstract void Draw(TFirstModel model);

    public abstract void Draw(TSecondModel model);
    public abstract void Draw(TThirdModel model);

    void IGameView.__Draw()
    {
        Draw();
        Draw(_gameController.GetFirstLayerViewModel());
        Draw(_gameController.GetSecondLayerViewModel());
        Draw(_gameController.GetThirdLayerViewModel());
    }
}
