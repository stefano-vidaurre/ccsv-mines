namespace CCSV.Games;
public interface IGameControllerCollection : IEnumerable<Type>
{
    Type? MainView { get; }

    public IGameControllerCollection AddGameController<TController>() where TController : class, IGameController;
    public IGameControllerCollection AddGameController<TController, TImplementation>() where TController : class, IGameController where TImplementation : class, TController;
    public IGameControllerViewMatcher BuildControllerViewMatcher();
}
