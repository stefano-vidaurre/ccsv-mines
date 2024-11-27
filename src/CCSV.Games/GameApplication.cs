using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public class GameApplication : IGameApplication
{
    private readonly IGameWindow _window;
    private readonly IServiceProvider _services;
    private readonly IGameControllerProvider _controllers;
    private Task _updateTask;

    public GameApplication(IGameWindow window, IServiceProvider services, IGameControllerProvider controllers)
    {
        _window = window;
        _services = services;
        _controllers = controllers;
        _updateTask = Task.CompletedTask;
    }

    public void Run()
    {
        IGameController? gameController = _controllers.GetMain();

        while (!_window.ShouldClose() && gameController is not null)
        {
            if (_updateTask.IsCompleted)
            {
                _updateTask = gameController.Update();
            }

            if (_window.IsNextFrame)
            {
                gameController.Draw();
            }
        }

        _window.Close();
    }
}
