namespace CCSV.Games;

public interface IGameWindowBuilder
{
    IGameWindowBuilder SetTitle(string title);
    IGameWindowBuilder SetSize(int width, int height);
    IGameWindowBuilder SetTargetFps(long targetFps);
    IGameWindow Build<TView>() where TView : IGameView;
}
