namespace CCSV.Games;
public interface IGameControllerViewMatcher
{
    IEnumerable<Type> Views { get; }
    IEnumerable<Type> Controllers { get; }

    Type? GetByView(Type tview);
    Type? GetByView<TView>() where TView : IGameView;
    Type? GetByView<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel;
}
