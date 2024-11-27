using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;
public class GameControllerProvider : IGameControllerProvider
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<Type> _controllerTypes;

    public GameControllerProvider(IServiceProvider serviceProvider, IEnumerable<Type> controllerTypes)
    {
        _serviceProvider = serviceProvider;
        _controllerTypes = controllerTypes;
    }

    public IGameController? GetController(Type type)
    {
        if (!_controllerTypes.Contains(type))
        {
            return default;
        }

        object? controller = _serviceProvider.GetService(type);

        return controller as IGameController;
    }

    public TController? GetController<TController>() where TController : IGameController
    {
        if (!_controllerTypes.Contains(typeof(TController)))
        {
            return default;
        }

        return _serviceProvider.GetService<TController>();
    }

    public IGameController? GetMain()
    {
        if (!_controllerTypes.Any())
        {
            return default;
        }

        object? controller = _serviceProvider.GetService(_controllerTypes.First());

        return controller as IGameController;
    }
}
