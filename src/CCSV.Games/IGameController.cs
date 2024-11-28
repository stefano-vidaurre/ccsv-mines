namespace CCSV.Games;
public interface IGameController
{
    void Draw();
}

public interface IGameController<out Vmodel> : IGameController where Vmodel : GameViewModel
{
    Vmodel GetViewModel();
}