using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CCSV.Games;
public class GameControllerCollection : IGameControllerCollection, IGameControllerViewCollection
{
    private readonly IServiceCollection _services;
    private readonly IDictionary<Type, Type> _controllerViewsTypes;

    public IEnumerable<Type> Keys => _controllerViewsTypes.Keys;

    public IEnumerable<Type> Values => _controllerViewsTypes.Values;

    public int Count => _controllerViewsTypes.Count;

    public Type this[Type key] => _controllerViewsTypes[key];

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

    private Type? GetViewType(Type tcontroller)
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

    public bool ContainsKey(Type key)
    {
        return _controllerViewsTypes.ContainsKey(key);
    }

    public bool TryGetValue(Type key, [MaybeNullWhen(false)] out Type value)
    {
        return _controllerViewsTypes.TryGetValue(key, out value);
    }

    IEnumerator<KeyValuePair<Type, Type>> IEnumerable<KeyValuePair<Type, Type>>.GetEnumerator()
    {
        return _controllerViewsTypes.GetEnumerator();
    }

    public Type? GetByView(Type tview)
    {
        if(!TryGetValue(tview, out Type? value))
        {
            return null;
        }
        
        return value;
    }

    public Type? GetByView<TView>()
    {
        return GetByView(typeof(TView));
    }
}
