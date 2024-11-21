using CCSV.Mines.GameApplications;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class MainView : GameView, IMainView
{
    private readonly IGameWindow _gameWindow;

    public MainView(IGameWindow window) : base(window)
    {
        _gameWindow = window;
    }

    public override void DrawView()
    {
        long fps = _gameWindow.Fps;
        long targetFps = _gameWindow.TargetFps;
        long delta = _gameWindow.LastDelta;
        long targetDelta = _gameWindow.TargetDelta;

        Raylib.ClearBackground(Color.White);
        Raylib.DrawText($"FPS: {fps}/{targetFps}", 12, 12, 20, Color.Black);
        Raylib.DrawText($"Delta: {delta}/{targetDelta}", 12, 42, 20, Color.Black);
    }
}
