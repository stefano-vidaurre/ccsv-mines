using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Mines.GameApplications;

public class GameApplication : IGameApplication
{
    private readonly IGameWindow _window;
    private readonly IServiceProvider _services;
    private Task _updateTask;

    public GameApplication(IGameWindow window, IServiceProvider services)
    {
        _window = window;
        _services = services;
        _updateTask = Task.CompletedTask;
    }

    public void Run()
    {
        IGameController? gameController = _services.GetService<IGameController>();

        while (!_window.ShouldClose() && gameController is not null)
        {
            if (_updateTask.IsCompleted)
            {
                _updateTask = gameController.Update();
            }

            if(_window.IsNextFrame)
            {
                gameController.Draw();
            }
        }

        _window.Close();
    }
}
