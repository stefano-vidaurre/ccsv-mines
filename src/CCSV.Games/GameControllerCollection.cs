using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace CCSV.Games;
public class GameControllerCollection : IGameControllerCollection
{
    private readonly IServiceCollection _services;
    private readonly IList<Type> _controllerTypes;

    public GameControllerCollection(IServiceCollection serviceDescriptors)
    {
        _services = serviceDescriptors;
        _controllerTypes = new List<Type>();
    }

    public IGameControllerCollection AddGameController<TController, TImplementation>()
        where TController : class, IGameController
        where TImplementation : class, TController
    {
        _services.AddSingleton<TController, TImplementation>();
        _controllerTypes.Add(typeof(TController));
        return this;
    }

    public IGameControllerCollection SetMain<TController>() where TController : class, IGameController
    {
        Type controllerType = typeof(TController);
        if(!_controllerTypes.Contains(controllerType))
        {
            throw new WrongOperationException($"The controller ({controllerType.Name}) is not registered.");
        }

        _controllerTypes.Remove(controllerType);
        _controllerTypes.Insert(0, controllerType);

        return this;
    }

    public IEnumerator<Type> GetEnumerator()
    {
        return _controllerTypes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _controllerTypes.GetEnumerator();
    }
}
