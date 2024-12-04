using CCSV.Domain.Exceptions;
using System.Reflection;

namespace CCSV.Games;
public class GameControllerViewMatcher : IGameControllerViewMatcher
{
    private readonly IReadOnlyDictionary<Type, Type> _dictionary;

    public IEnumerable<Type> Views => _dictionary.Keys.AsEnumerable();

    public IEnumerable<Type> Controllers => _dictionary.Values.AsEnumerable();

    public GameControllerViewMatcher(IReadOnlyDictionary<Type, Type> dictionary)
    {
        _dictionary = dictionary;
    }

    public Type? GetByView(Type tview)
    {
        if(!IsGameView(tview))
        {
            throw new BusinessException($"Type ({tview.Name}) doesnt implemet {nameof(IGameView)} interface.");
        }

        if(!_dictionary.TryGetValue(tview, out Type? result))
        {
            return null;
        }

        return result;
    }

    public Type? GetByView<TView>() where TView : IGameView
    {
        return GetByView(typeof(TView));
    }

    public Type? GetByView<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel
    {
        return GetByView(typeof(TView));
    }

    private static bool IsGameView(Type type)
    {
        return !(type.GetInterface(nameof(IGameView)) is null && type.GetInterface(typeof(IGameView<>).Name) is null);
    }
}
