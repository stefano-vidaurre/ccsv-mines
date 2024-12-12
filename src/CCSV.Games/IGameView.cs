namespace CCSV.Games;
public interface IGameView
{
    void Draw();
}

public interface IGameView<in TFirstModel> : IGameView where TFirstModel : GameViewModel
{
    void Draw(TFirstModel model);
}

public interface IGameView<in TFirstModel, in TSecondModel> : IGameView<TFirstModel> where TFirstModel : GameViewModel where TSecondModel : GameViewModel
{
    void Draw(TSecondModel model);
}

public interface IGameView<in TFirstModel, in TSecondModel, in TThirdModel> : IGameView<TFirstModel, TSecondModel> where TFirstModel : GameViewModel where TSecondModel : GameViewModel where TThirdModel : GameViewModel
{
    void Draw(TThirdModel model);
}
