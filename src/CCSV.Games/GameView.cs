namespace CCSV.Games;
public abstract class GameView : IGameView
{
    private readonly IGameWindow _window;

    protected GameView(IGameWindow window)
    {
        _window = window;
    }

    public abstract void DrawView();

    public void Draw()
    {
        _window.BeginDrawing();
        DrawView();
        _window.EndDrawing();
    }
}
