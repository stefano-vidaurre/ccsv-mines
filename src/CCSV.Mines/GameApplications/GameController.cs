namespace CCSV.Mines.GameApplications;
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

    public Task Update()
    {
        return Task.Run(() =>
        {
            while (true) ;
        });
    }
}
