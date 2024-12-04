using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public abstract class GameApplicationBuilder : IGameApplicationBuilder
{
    private readonly GameControllerCollection _gameControllers;

    public IGameWindowBuilder Window { get; private set; }

    public IServiceCollection Services { get; private set; }

    public IGameControllerCollection Controllers => _gameControllers;

    protected GameApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services)
    {
        Window = windowBuilder;
        Services = services;
        _gameControllers = new GameControllerCollection(services);
    }

    public IGameApplication Build()
    {
        if(Window.MainView is null && Controllers.Any())
        {
            Type controllerType = _gameControllers.Keys.First();
            Window.SetMainView(controllerType);
        }

        IGameWindow window = Window.Build();

        Services.AddSingleton(window);
        Services.AddSingleton<IGameApplication, GameApplication>();
        Services.AddSingleton<IGameEventHandler, GameEventHandler>();
        Services.AddSingleton<IGameControllerViewCollection>(_gameControllers);
        IServiceProvider services = Services.BuildServiceProvider();

        return services.GetService<IGameApplication>() ?? throw new BusinessException("Error building game application.");
    }
}
