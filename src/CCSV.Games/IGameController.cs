namespace CCSV.Games;
public interface IGameController
{
    void Draw();
}

public interface IGameController<out TFirstModel> : IGameController where TFirstModel : GameViewModel
{
    TFirstModel GetFirstLayerViewModel();
}

public interface IGameController<out TFirstModel, out TSecondModel> : IGameController<TFirstModel> where TFirstModel : GameViewModel where TSecondModel : GameViewModel
{
    TSecondModel GetSecondLayerViewModel();
}

public interface IGameController<out TFirstModel, out TSecondModel, out TThirdModel> : IGameController<TFirstModel, TSecondModel> where TFirstModel : GameViewModel where TSecondModel : GameViewModel where TThirdModel : GameViewModel
{
    TThirdModel GetThirdLayerViewModel();
}