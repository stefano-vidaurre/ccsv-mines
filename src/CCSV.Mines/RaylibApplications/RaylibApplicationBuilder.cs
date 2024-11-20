using CCSV.Mines.GameApplications;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Mines.RaylibApplications;

public class RaylibApplicationBuilder : IGameApplicationBuilder
{
    public IGameWindowBuilder Window { get; private set; }

    public IServiceCollection Services { get; private set; }

    private RaylibApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services)
    {
        Window = windowBuilder;
        Services = services;
    }

    public static IGameApplicationBuilder CreateBuilder()
    {
        IGameWindowBuilder windowBuilder = new RaylibWindowBuilder();
        IServiceCollection services = new ServiceCollection();

        return new RaylibApplicationBuilder(windowBuilder, services);
    }

    public IGameApplication Build()
    {
        IGameWindow window = Window.Build();

        Services.AddSingleton((RaylibWindow)window);

        IServiceProvider services = Services.BuildServiceProvider();

        return new GameApplication(window, services);
    }
}
