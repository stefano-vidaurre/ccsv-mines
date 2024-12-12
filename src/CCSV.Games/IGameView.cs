namespace CCSV.Games;
public interface IGameView
{
#pragma warning disable IDE1006 // Estilos de nombres
    internal void __Draw();
    internal IGameController? __Controller { get; }
#pragma warning restore IDE1006 // Estilos de nombres
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
