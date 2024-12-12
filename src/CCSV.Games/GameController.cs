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

public abstract class GameController<TFirstModel> : IGameController<TFirstModel> where TFirstModel : GameViewModel
{
    private readonly IGameView<TFirstModel> _gameView;

    protected GameController(IGameView<TFirstModel> gameView)
    {
        _gameView = gameView;
    }

    public abstract TFirstModel GetFirstLayerViewModel();

    public void Draw()
    {
        TFirstModel model = GetFirstLayerViewModel();
        _gameView.Draw();
        _gameView.Draw(model);
    }
}

public abstract class GameController<TFirstModel, TSecondModel> : IGameController<TFirstModel, TSecondModel> where TFirstModel : GameViewModel where TSecondModel : GameViewModel
{
    private readonly IGameView<TFirstModel, TSecondModel> _gameView;

    protected GameController(IGameView<TFirstModel, TSecondModel> gameView)
    {
        _gameView = gameView;
    }

    public abstract TFirstModel GetFirstLayerViewModel();
    public abstract TSecondModel GetSecondLayerViewModel();

    public void Draw()
    {
        TFirstModel firstModel = GetFirstLayerViewModel();
        TSecondModel secondModel = GetSecondLayerViewModel();
        _gameView.Draw();
        _gameView.Draw(firstModel);
        _gameView.Draw(secondModel);
    }

}

public abstract class GameController<TFirstModel, TSecondModel, TThirdModel> : IGameController<TFirstModel, TSecondModel, TThirdModel> where TFirstModel : GameViewModel where TSecondModel : GameViewModel where TThirdModel : GameViewModel
{
    private readonly IGameView<TFirstModel, TSecondModel, TThirdModel> _gameView;

    protected GameController(IGameView<TFirstModel, TSecondModel, TThirdModel> gameView)
    {
        _gameView = gameView;
    }

    public abstract TFirstModel GetFirstLayerViewModel();
    public abstract TSecondModel GetSecondLayerViewModel();
    public abstract TThirdModel GetThirdLayerViewModel();

    public void Draw()
    {
        TFirstModel firstModel = GetFirstLayerViewModel();
        TSecondModel secondModel = GetSecondLayerViewModel();
        TThirdModel thirdModel = GetThirdLayerViewModel();
        _gameView.Draw();
        _gameView.Draw(firstModel);
        _gameView.Draw(secondModel);
        _gameView.Draw(thirdModel);
    }

}
