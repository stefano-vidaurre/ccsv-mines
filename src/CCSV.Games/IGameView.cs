namespace CCSV.Games;
public interface IGameView
{
    void Draw();
}

public interface IGameView<in Vmodel> : IGameView where Vmodel : GameViewModel
{
    void Draw(Vmodel model);
}
