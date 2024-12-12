using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CCSV.Games;
public class GameEventHandler : IGameEventHandler
{
    private readonly ILogger<GameEventHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IGameWindow _window;
    private IGameController? _gameController;
    private IServiceScope _serviceScope;
    private IGameView _gameView;
    private Type _currentViewType;
    private bool _firstUpdate;

    public GameEventHandler(IGameWindow window, IServiceProvider serviceProvider, ILogger<GameEventHandler> logger)
    {
        _window = window;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _currentViewType = _window.CurrentViewType;
        _serviceScope = _serviceProvider.CreateScope();
        _gameView = _serviceScope.ServiceProvider.GetService(_currentViewType) as IGameView ?? throw new BusinessException();
        _gameController = _gameView.__Controller;
        _firstUpdate = true;
    }

    public void Draw()
    {
        _window.BeginDrawing();
        _gameView.__Draw();
        _window.EndDrawing();
        _firstUpdate = true;
    }

    public Task Update()
    {
        if (_currentViewType != _window.CurrentViewType)
        {
            _logger.LogInformation("Changing the view: {0} -> {1}", _currentViewType.Name, _window.CurrentViewType.Name);
            _serviceScope.Dispose();
            _serviceScope = _serviceProvider.CreateScope();
            _currentViewType = _window.CurrentViewType;
            _gameView = _serviceScope.ServiceProvider.GetService(_currentViewType) as IGameView ?? throw new BusinessException();
            _gameController = _gameView.__Controller;
        }

        if(_gameController is null)
        {
            return Task.CompletedTask;
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
