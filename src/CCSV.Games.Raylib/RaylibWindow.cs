using CCSV.Domain.Exceptions;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;

public class RaylibWindow : IGameWindow
{
    private const long OneSecond = TimeSpan.TicksPerSecond;
    private static RaylibWindow? _instance = null;

    public static bool IsInitialised => _instance != null;

    public string Title { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public long TargetFps { get; private set; }
    public long TargetDelta { get; private set; }
    public long LastDelta { get; private set; }
    public DateTime DrawingSince { get; private set; }
    public bool IsClosed { get; private set; }
    public bool IsDrawing { get; private set; }

    public long Fps => OneSecond / LastDelta;
    public long Delta => DateTime.UtcNow.Ticks - DrawingSince.Ticks;
    public bool IsNextFrame => Delta > TargetDelta;

    private RaylibWindow()
    {
        Title = "";
        Width = 0;
        Height = 0;
        TargetFps = 60;
        TargetDelta = OneSecond / TargetFps;
        LastDelta = TargetDelta;
        IsDrawing = false;
        IsClosed = false;
        DrawingSince = DateTime.UtcNow;
    }

    private RaylibWindow(string title, int width, int height, long targetFps)
    {
        Title = title;
        Width = width;
        Height = height;
        TargetFps = targetFps;
        TargetDelta = OneSecond / TargetFps;
        LastDelta = TargetDelta;
        IsDrawing = false;
        IsClosed = false;
        DrawingSince = DateTime.UtcNow;
        RaylibService.InitWindow(width, height, title);
    }

    public static IGameWindow Create(string title, int width, int height, long targetFps)
    {
        if (_instance is not null)
        {
            _instance.Close();
            throw new WrongOperationException("The window is already initialised.");
        }

        if (targetFps < 1)
        {
            throw new WrongOperationException("Target FPS value cant be less than 1.");
        }

        _instance = new RaylibWindow(title, width, height, targetFps);
        return _instance;
    }

    public void BeginDrawing()
    {
        IsDrawing = true;
        LastDelta = Delta;
        DrawingSince = DateTime.UtcNow;
        RaylibService.BeginDrawing();
    }

    public void EndDrawing()
    {
        RaylibService.EndDrawing();
        IsDrawing = false;
    }

    public bool ShouldClose()
    {
        return RaylibService.WindowShouldClose();
    }

    public void Close()
    {
        RaylibService.CloseWindow();
        IsClosed = true;
    }
}
