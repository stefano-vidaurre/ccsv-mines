namespace CCSV.Games;
public interface IGameEventHandler
{
    Task Update();
    void Draw();
}
