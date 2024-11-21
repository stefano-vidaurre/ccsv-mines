using CCSV.Mines.GameApplications;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Mines.RaylibApplications;

public abstract class GameApplicationBuilder : IGameApplicationBuilder
{
    public IGameWindowBuilder Window { get; private set; }

    public IServiceCollection Services { get; private set; }

    protected GameApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services)
    {
        Window = windowBuilder;
        Services = services;
    }

    public IGameApplication Build()
    {
        IGameWindow window = Window.Build();

        Services.AddSingleton(window);

        IServiceProvider services = Services.BuildServiceProvider();

        return new GameApplication(window, services);
    }
}
