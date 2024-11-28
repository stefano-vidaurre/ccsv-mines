using CCSV.Domain.Exceptions;
using System.Reflection;

namespace CCSV.Games;
public abstract class GameEventHandler : IGameEventHandler
{
    private readonly IGameWindow _window;
    private readonly IGameControllerProvider _controllers;
    private readonly IGameController _gameController;
    private bool _firstUpdate;

    protected GameEventHandler(IGameWindow window, IGameControllerProvider controllers)
    {
        _window = window;
        _controllers = controllers;
        _gameController = _controllers.GetMain() ?? throw new WrongOperationException("There is no main controller.");
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

            if (!HasToBeInvoked(attribute))
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

    protected abstract bool HasToBeInvoked(GameEventAttribute attribute);
}
