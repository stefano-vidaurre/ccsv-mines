using CCSV.Domain.Exceptions;
using System.Reflection;

namespace CCSV.Games;
public class GameEventHandler : IGameEventHandler
{
    private readonly IGameWindow _window;
    private readonly IServiceProvider _services;
    private readonly IGameControllerViewCollection _controllerViewCollection;
    private IGameController _gameController;
    private Type _currentControllerType;
    private Type _currentViewType;
    private bool _firstUpdate;

    public GameEventHandler(IGameWindow window, IServiceProvider services, IGameControllerViewCollection controllerViewCollection)
    {
        _window = window;
        _services = services;
        _controllerViewCollection = controllerViewCollection;
        _currentViewType = _window.CurrentViewType;
        _currentControllerType = _controllerViewCollection.GetByView(_currentViewType) ?? throw new WrongOperationException($"View ({_currentViewType.Name}) is not registered in DI service.");
        _gameController = _services.GetService(_currentControllerType) as IGameController ?? throw new WrongOperationException($"Controller ({_currentControllerType.Name}) is not registered in DI service.");
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
        if(_currentViewType != _window.CurrentViewType)
        {
            _currentViewType = _window.CurrentViewType;
            _currentControllerType = _controllerViewCollection.GetByView(_currentViewType) ?? throw new WrongOperationException($"View ({_currentViewType.Name}) is not registered in DI service.");
            _gameController = _services.GetService(_currentControllerType) as IGameController ?? throw new WrongOperationException($"Controller ({_currentControllerType.Name}) is not registered in DI service.");
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
