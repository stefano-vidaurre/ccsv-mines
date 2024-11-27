namespace CCSV.Games;
public interface IGameControllerProvider
{
    IGameController? GetMain();
    IGameController? GetController(Type type);
    TController? GetController<TController>() where TController : IGameController;
}
