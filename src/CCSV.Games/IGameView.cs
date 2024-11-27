namespace CCSV.Games;
public interface IGameView
{
    IGameWindow Window { get; }
    void Draw();
}

public interface IGameView<in Vmodel> where Vmodel : GameViewModel
{
    IGameWindow Window { get; }
    void Draw(Vmodel model);
}
