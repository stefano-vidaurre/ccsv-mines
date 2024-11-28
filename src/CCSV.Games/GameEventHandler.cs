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
        return Task.Run(() =>
        {
            MethodInfo[] methods = _gameController.GetType().GetMethods();

            foreach (MethodInfo method in methods)
            {
                GameEventAttribute? attribute = Attribute.GetCustomAttribute(method, typeof(GameEventAttribute)) as GameEventAttribute;
                if (attribute is null)
                {
                    continue;
                }

                if(!HasToBeInvoked(attribute))
                {
                    continue;
                }

                if(method.GetParameters().Length == 0)
                {
                    method.Invoke(_gameController, null);
                    continue;
                }

                if (method.GetParameters().Length == 1)
                {
                    method.Invoke(_gameController, [_window.Delta]);
                    continue;
                }

                if (method.GetParameters().Length == 2)
                {
                    method.Invoke(_gameController, [_window.Delta, _firstUpdate]);
                    continue;
                }
            }

            _firstUpdate = false;
        });
    }

    protected abstract bool HasToBeInvoked(GameEventAttribute attribute);
}
