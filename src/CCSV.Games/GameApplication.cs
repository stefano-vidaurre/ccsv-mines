using Microsoft.Extensions.Logging;

namespace CCSV.Games;

public class GameApplication : IGameApplication
{
    private readonly IGameWindow _window;
    private readonly IGameEventHandler _gameEventHandler;
    private readonly ILogger<GameApplication> _logger;
    private Task _updateTask;

    public GameApplication(IGameWindow window, IGameEventHandler gameEventHandler, ILogger<GameApplication> logger)
    {
        _window = window;
        _updateTask = Task.CompletedTask;
        _gameEventHandler = gameEventHandler;
        _logger = logger;
    }

    public void Run()
    {
        _logger.LogInformation("Application runing...");
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
        _logger.LogInformation("Application is finished.");
    }
}
