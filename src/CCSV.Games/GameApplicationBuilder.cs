using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public abstract class GameApplicationBuilder : IGameApplicationBuilder
{
    public IGameWindowBuilder Window { get; private set; }

    public IServiceCollection Services { get; private set; }

    protected GameApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services)
    {
        Window = windowBuilder;
        Services = services;
    }

    public IGameApplication Build<TView>() where TView : IGameView
    {
        IGameWindow window = Window.Build<TView>();

        Services.AddSingleton(window);
        Services.AddSingleton<IGameApplication, GameApplication>();
        Services.AddSingleton<IGameEventHandler, GameEventHandler>();
        IServiceProvider services = Services.BuildServiceProvider();

        return services.GetService<IGameApplication>() ?? throw new BusinessException("Error building game application.");
    }
}
