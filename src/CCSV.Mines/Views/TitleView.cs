using CCSV.Games;
using CCSV.Games.Raylib;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class TitleView : GameView
{
    private readonly IGameWindow _gameWindow;
    private readonly int _fontSize;

    public TitleView(IGameWindow gameWindow)
    {
        _gameWindow = gameWindow;
        _fontSize = 20;
    }

    [KeyboardPressed(KeyboardKey.Space)]
    public Task OnSpaceKeyPressed()
    {
        if(Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            _gameWindow.NextView<MainView>();
        }

        return Task.CompletedTask;
    }

    public override void Draw()
    {
        Raylib.ClearBackground(Color.White);
        Raylib.DrawText($"Pulse space", (_gameWindow.Width / 2) - (_fontSize * 3), (_gameWindow.Height / 2) - _fontSize, _fontSize, Color.Black);
    }
}
