using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CCSV.Games;
public class GameControllerCollection : IGameControllerCollection
{
    private readonly IServiceCollection _services;
    private readonly IDictionary<Type, Type> _controllerViewsTypes;

    public Type? MainView => _controllerViewsTypes.Keys.FirstOrDefault();

    public GameControllerCollection(IServiceCollection serviceDescriptors)
    {
        _services = serviceDescriptors;
        _controllerViewsTypes = new Dictionary<Type, Type>();
    }

    public IGameControllerCollection AddGameController<TController, TImplementation>()
        where TController : class, IGameController
        where TImplementation : class, TController
    {
        Type controllerType = typeof(TController);
        Type? viewType = GetViewType(controllerType);

        if (viewType is null)
        {
            throw new WrongOperationException($"Controller ({nameof(TController)}) has not any view.");
        }

        _services.AddSingleton<TController, TImplementation>();
        _controllerViewsTypes.Add(viewType, controllerType);
        return this;
    }

    public IGameControllerCollection AddGameController<TController>()
        where TController : class, IGameController
    {
        Type controllerType = typeof(TController);
        Type? viewType = GetViewType(controllerType);

        if (viewType is null)
        {
            throw new WrongOperationException($"Controller ({nameof(TController)}) has not any view.");
        }

        _services.AddSingleton<TController>();
        _controllerViewsTypes.Add(viewType, controllerType);
        return this;
    }

    private static Type? GetViewType(Type tcontroller)
    {
        ConstructorInfo[] ctors = tcontroller.GetConstructors();

        if (ctors.Length == 0)
        {
            throw new WrongOperationException($"Controller ({tcontroller.Name}) has not any public constructors.");
        }

        ConstructorInfo constructorInfo = ctors[0];
        ParameterInfo? viewParameter = Array.Find(constructorInfo.GetParameters(), param => IsGameView(param));

        if (viewParameter is not null)
        {
            return viewParameter.ParameterType;
        }

        GameViewAttribute? attribute = Attribute.GetCustomAttribute(tcontroller, typeof(GameViewAttribute)) as GameViewAttribute;
        if (attribute is null)
        {
            throw new WrongOperationException($"Controller ({tcontroller.Name}) has not any view.");
        }

        return attribute.ViewType;
    }

    private static bool IsGameView(ParameterInfo param)
    {
        return !(param.ParameterType.GetInterface(nameof(IGameView)) is null && param.ParameterType.GetInterface(typeof(IGameView<>).Name) is null);
    }

    public IEnumerator<Type> GetEnumerator()
    {
        return _controllerViewsTypes.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _controllerViewsTypes.Values.GetEnumerator();
    }

    public IGameControllerViewMatcher BuildControllerViewMatcher()
    {
        ReadOnlyDictionary<Type, Type> dictionary = _controllerViewsTypes.AsReadOnly();

        return new GameControllerViewMatcher(dictionary);
    }
}
