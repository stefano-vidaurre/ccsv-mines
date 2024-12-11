namespace CCSV.Games;
public interface IGameControllerProvider
{
    IGameController BuildController<TView>() where TView : IGameView;
    IGameController BuildController(Type tview);
}
