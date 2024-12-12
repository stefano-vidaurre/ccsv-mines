using CCSV.Domain.Exceptions;

namespace CCSV.Games.Raylib;

public class RaylibWindowBuilder : IGameWindowBuilder
{
    private string _title;
    private int _width;
    private int _height;
    private long _targetFps;

    public RaylibWindowBuilder()
    {
        _title = "Window title";
        _width = 800;
        _height = 480;
        _targetFps = 60;
    }

    public IGameWindow Build<TView>() where TView : IGameView
    {
        return RaylibWindow.Create(_title, _width, _height, _targetFps, typeof(TView));
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
}
