namespace CCSV.Games;

public class GameApplication : IGameApplication
{
    private readonly IGameWindow _window;
    private readonly IGameEventHandler _gameEventHandler;
    private Task _updateTask;

    public GameApplication(IGameWindow window, IGameEventHandler gameEventHandler)
    {
        _window = window;
        _updateTask = Task.CompletedTask;
        _gameEventHandler = gameEventHandler;
    }

    public void Run()
    {
        while (!_window.ShouldClose())
        {
            if (_updateTask.IsCompleted)
            {
                _updateTask = _gameEventHandler.Update();
            }

            if (_window.IsNextFrame)
            {
                _gameEventHandler.Draw();
            }
        }

        _window.Close();
    }
}
