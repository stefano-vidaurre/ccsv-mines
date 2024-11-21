namespace CCSV.Mines.GameApplications;

public interface IGameWindowBuilder
{
    IGameWindowBuilder SetTitle(string title);
    IGameWindowBuilder SetSize(int width, int height);
    IGameWindowBuilder SetTargetFps(long targetFps);
    IGameWindow Build();
}
