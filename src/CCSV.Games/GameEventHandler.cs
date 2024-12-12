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
        _gameView = _serviceScope.ServiceProvider.GetService(_currentViewType) as IGameView ?? throw new WrongOperationException($"View ({_currentViewType.Name}) is not registered.");
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
            OpenNextView();
        }

        if (_gameController is null)
        {
            IEnumerable<Task> tasks = HandleGameEvents(_gameView);
            _firstUpdate = false;
            return Task.WhenAll(tasks);
        }

        
        IEnumerable<Task> controllerTasks = HandleGameEvents(_gameController);
        IEnumerable<Task> viewTasks = HandleGameEvents(_gameView);
        _firstUpdate = false;
        return Task.WhenAll(controllerTasks.Concat(viewTasks));
    }

    private void OpenNextView()
    {
        _logger.LogDebug("Changing the view: {0} -> {1}", _currentViewType.Name, _window.CurrentViewType.Name);
        _serviceScope.Dispose();
        _serviceScope = _serviceProvider.CreateScope();
        _currentViewType = _window.CurrentViewType;
        _gameView = _serviceScope.ServiceProvider.GetService(_currentViewType) as IGameView ?? throw new WrongOperationException($"View ({_currentViewType.Name}) is not registered.");
        _gameController = _gameView.__Controller;
    }


    private IEnumerable<Task> HandleGameEvents<T>(T handler)
    {
        IList<Task> tasks = new List<Task>();

        if (handler is null)
        {
            return tasks;
        }

        Type handlerType = handler.GetType();

        foreach (MethodInfo method in handlerType.GetMethods())
        {
            tasks.Add(HandleMethod(handler, method));
        }

        return tasks;
    }

    private Task HandleMethod<T>(T handler, MethodInfo method)
    {
        GameEventAttribute? attribute = Attribute.GetCustomAttribute(method, typeof(GameEventAttribute)) as GameEventAttribute;
        if (attribute is null)
        {
            return Task.CompletedTask;
        }

        DesyncEventAttribute? desyncAttribute = Attribute.GetCustomAttribute(method, typeof(DesyncEventAttribute)) as DesyncEventAttribute;
        if (!_firstUpdate && desyncAttribute is null)
        {
            return Task.CompletedTask;
        }

        if (!attribute.HasHappened())
        {
            return Task.CompletedTask;
        }

        ParameterInfo[] parameters = method.GetParameters();

        if (parameters.Length == 0)
        {
            Task? task = method.Invoke(handler, null) as Task;

            if (task is null)
            {
                return Task.CompletedTask;
            }

            return task;
        }

        if (parameters.Length == 1 && parameters[0].ParameterType == _window.LastDelta.GetType())
        {
            Task? task = method.Invoke(handler, [_window.LastDelta]) as Task;

            if (task is null)
            {
                return Task.CompletedTask;
            }

            return task;
        }

        return Task.CompletedTask;
    }
}
