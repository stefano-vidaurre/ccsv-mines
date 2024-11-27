namespace CCSV.Games;
public interface IGameController
{
    Task Update();
    void Draw();
}

public interface IGameController<out Vmodel> : IGameController where Vmodel : GameViewModel
{
    Vmodel GetViewModel();
}