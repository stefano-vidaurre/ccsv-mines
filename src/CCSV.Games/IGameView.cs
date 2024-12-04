namespace CCSV.Games;
public interface IGameView
{
    void Draw();
}

public interface IGameView<in Vmodel> where Vmodel : GameViewModel
{
    void Draw(Vmodel model);
}
