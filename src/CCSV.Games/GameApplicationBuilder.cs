using CCSV.Domain.Exceptions;
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
        Services.AddSingleton<IGameControllerProvider>((services) => new GameControllerProvider(services, Controllers));
        Services.AddSingleton<IGameApplication, GameApplication>();
        Services.AddSingleton<IGameEventHandler, GameEventHandler>();
        IServiceProvider services = Services.BuildServiceProvider();

        return services.GetService<IGameApplication>() ?? throw new BusinessException("Error building game application.");
    }
}
