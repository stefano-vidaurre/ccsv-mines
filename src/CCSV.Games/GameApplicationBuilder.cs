using CCSV.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public abstract class GameApplicationBuilder : IGameApplicationBuilder
{
    public IGameWindowBuilder Window { get; private set; }

    public IServiceCollection Services { get; private set; }

    public IConfiguration Configuration { get; private set; }

    public IGameEnvironment Environment { get; private set; }

    protected GameApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services, IConfiguration configuration, IGameEnvironment environment)
    {
        Configuration = configuration;
        Window = windowBuilder;
        Services = services;
        Environment = environment;
    }

    public IGameApplication Build<TView>() where TView : IGameView
    {
        IGameWindow window = Window.Build<TView>();

        Services.AddSingleton(window);
        Services.AddSingleton<IGameApplication, GameApplication>();
        Services.AddSingleton<IGameEventHandler, GameEventHandler>();
        Services.AddSingleton<IConfiguration>(Configuration);
        Services.AddSingleton<IGameEnvironment>(Environment);
        IServiceProvider services = Services.BuildServiceProvider();

        return services.GetService<IGameApplication>() ?? throw new BusinessException("Error building game application.");
    }
}
