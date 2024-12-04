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
        if(Window.MainView is null && Controllers.MainView is not null)
        {
            Type viewType = Controllers.MainView;
            Window.SetMainView(viewType);
        }

        IGameWindow window = Window.Build();
        IGameControllerViewMatcher matcher = Controllers.BuildControllerViewMatcher();

        Type? unregisteredView = matcher.Views.FirstOrDefault(view => !Services.Any(item => item.ServiceType.Equals(view)));

        if (unregisteredView is not null)
        {
            throw new WrongOperationException($"The view {unregisteredView.Name} is not registered in DI.");
        }

        Services.AddSingleton(window);
        Services.AddSingleton(matcher);
        Services.AddSingleton<IGameApplication, GameApplication>();
        Services.AddSingleton<IGameEventHandler, GameEventHandler>();
        Services.AddSingleton<IGameControllerProvider, GameControllerProvider>();
        IServiceProvider services = Services.BuildServiceProvider();

        return services.GetService<IGameApplication>() ?? throw new BusinessException("Error building game application.");
    }
}
