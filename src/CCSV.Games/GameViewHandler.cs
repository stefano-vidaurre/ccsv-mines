namespace CCSV.Games;
public class GameViewHandler
{
    private readonly IGameWindow _window;

    private readonly IGameView _view;

    public GameViewHandler(IGameWindow window, IGameView view)
    {
        _window = window;
        _view = view;
    }

    public void Draw()
    {
        _window.BeginDrawing();
        _view.Draw();
        _window.EndDrawing();
    }
}

public class GameViewHandler<Vmodel> where Vmodel : GameViewModel
{
    private readonly IGameWindow _window;
    private readonly IGameView<Vmodel> _view;

    public GameViewHandler(IGameWindow window, IGameView<Vmodel> view)
    {
        _window = window;
        _view = view;
    }

    public void Draw(Vmodel model)
    {
        _window.BeginDrawing();
        _view.Draw(model);
        _window.EndDrawing();
    }
}
