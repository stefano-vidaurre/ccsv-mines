using CCSV.Games;
using CCSV.Mines.Controllers;
using CCSV.Mines.Models;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class MainView : GameView<BallViewModel>
{
    private readonly IGameWindow _window;

    public MainView(IGameWindow window, MainController mainController) : base(mainController)
    {
        _window = window;
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.White);
    }

    public override void Draw(BallViewModel model)
    {
        long fps = _window.Fps;
        long targetFps = _window.TargetFps;
        long delta = _window.LastDelta;
        long targetDelta = _window.TargetDelta;

        
        Raylib.DrawText($"FPS: {fps}/{targetFps}", 12, 12, 20, Color.Black);
        Raylib.DrawText($"Delta: {delta}/{targetDelta}", 12, 42, 20, Color.Black);
        Raylib.DrawCircle(model.PosX, model.PosY, model.Radius, Color.Black);
    }
}
