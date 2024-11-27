using CCSV.Games;
using CCSV.Mines.Models;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class MainView : IMainView
{
    public IGameWindow Window { get; private set; }

    public MainView(IGameWindow window)
    {
        Window = window;
    }

    public void Draw(BallViewModel model)
    {
        long fps = Window.Fps;
        long targetFps = Window.TargetFps;
        long delta = Window.LastDelta;
        long targetDelta = Window.TargetDelta;

        Raylib.ClearBackground(Color.White);
        Raylib.DrawText($"FPS: {fps}/{targetFps}", 12, 12, 20, Color.Black);
        Raylib.DrawText($"Delta: {delta}/{targetDelta}", 12, 42, 20, Color.Black);
        Raylib.DrawCircle(model.PosX, model.PosY, model.Radius, Color.Black);
    }
}
