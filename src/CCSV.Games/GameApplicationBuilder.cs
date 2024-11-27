using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public abstract class GameApplicationBuilder : IGameApplicationBuilder
{
    public IGameWindowBuilder Window { get; private set; }

    public IServiceCollection Services { get; private set; }

    public IGameControllerCollection Controllers { get; private set; }

    protected GameApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services)
    {
        Window = windowBuilder;
        Services = services;
        Controllers = new GameControllerCollection(services);
    }

    public IGameApplication Build()
    {
        IGameWindow window = Window.Build();

        Services.AddSingleton(window);

        IServiceProvider services = Services.BuildServiceProvider();
        IGameControllerProvider controllers = new GameControllerProvider(services, Controllers);

        return new GameApplication(window, services, controllers);
    }
}
