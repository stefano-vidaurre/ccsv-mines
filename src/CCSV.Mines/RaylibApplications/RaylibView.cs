using CCSV.Mines.GameApplications;

namespace CCSV.Mines.RaylibApplications;
public abstract class RaylibView : IGameView
{
    private readonly RaylibWindow _window;

    protected RaylibView(RaylibWindow window)
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
