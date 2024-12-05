using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CCSV.Games;
public class GameEventHandler : IGameEventHandler
{
    private readonly ILogger<GameEventHandler> _logger;
    private readonly IGameWindow _window;
    private readonly IGameControllerProvider _controllerProvider;
    private IGameController _gameController;
    private Type _currentViewType;
    private bool _firstUpdate;

    public GameEventHandler(IGameWindow window, IGameControllerProvider controllerProvider, ILogger<GameEventHandler> logger)
    {
        _window = window;
        _logger = logger;
        _controllerProvider = controllerProvider;
        _currentViewType = _window.CurrentViewType;
        _gameController = _controllerProvider.BuildController(_currentViewType);
        _firstUpdate = true;
    }

    public void Draw()
    {
        _window.BeginDrawing();
        _gameController.Draw();
        _window.EndDrawing();
        _firstUpdate = true;
    }

    public Task Update()
    {
        if (_currentViewType != _window.CurrentViewType)
        {
            _logger.LogInformation("Changing the view: {0} -> {1}", _currentViewType.Name, _window.CurrentViewType.Name);
            _currentViewType = _window.CurrentViewType;
            _gameController = _controllerProvider.BuildController(_currentViewType);
        }

        MethodInfo[] methods = _gameController.GetType().GetMethods();
        if (!_firstUpdate)
        {
            return Task.CompletedTask;
        }

        IList<Task> tasks = new List<Task>();

        foreach (MethodInfo method in methods)
        {
            GameEventAttribute? attribute = Attribute.GetCustomAttribute(method, typeof(GameEventAttribute)) as GameEventAttribute;
            if (attribute is null)
            {
                continue;
            }

            if (!attribute.HasHappened())
            {
                continue;
            }

            _logger.LogTrace("[{0}] event has happened", attribute.Name);

            if (method.GetParameters().Length == 0)
            {
                Task? task = method.Invoke(_gameController, null) as Task;

                if (task is not null)
                {
                    tasks.Add(task);
                }

                continue;
            }

            if (method.GetParameters().Length == 1)
            {
                Task? task = method.Invoke(_gameController, [_window.LastDelta]) as Task;

                if (task is not null)
                {
                    tasks.Add(task);
                }

                continue;
            }
        }

        _firstUpdate = false;
        return Task.WhenAll(tasks);
    }
}
