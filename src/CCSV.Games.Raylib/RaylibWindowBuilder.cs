using CCSV.Domain.Exceptions;

namespace CCSV.Games.Raylib;

public class RaylibWindowBuilder : IGameWindowBuilder
{
    private string _title;
    private int _width;
    private int _height;
    private long _targetFps;

    public Type? MainView { get; private set; }

    public RaylibWindowBuilder()
    {
        _title = "Window title";
        _width = 800;
        _height = 480;
        _targetFps = 60;
        MainView = null;
    }

    public IGameWindow Build()
    {
        if (MainView is null)
        {
            throw new BusinessException("Main controller is not setted.");
        }

        return RaylibWindow.Create(_title, _width, _height, _targetFps, MainView);
    }

    public IGameWindowBuilder SetSize(int width, int height)
    {
        _width = width;
        _height = height;

        return this;
    }

    public IGameWindowBuilder SetTitle(string title)
    {
        _title = title;

        return this;
    }

    public IGameWindowBuilder SetTargetFps(long targetFps)
    {
        if (targetFps < 1)
        {
            throw new InvalidValueException("Target FPS value cant be less than 1.");
        }

        _targetFps = targetFps;

        return this;
    }

    public IGameWindowBuilder SetMainView<TView>() where TView : IGameView
    {
        MainView = typeof(TView);
        return this;
    }

    public IGameWindowBuilder SetMainView(Type tview)
    {
        if (tview.GetInterface(nameof(IGameView)) is null)
        {
            throw new InvalidValueException($"The type ({tview.Name}) doesnt implement the {nameof(IGameView)} interface.");
        }

        MainView = tview;
        return this;
    }
}
