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
    public Type CurrentViewType { get; private set; }

    public long Fps => OneSecond / LastDelta;
    public long Delta => DateTime.UtcNow.Ticks - DrawingSince.Ticks;
    public bool IsNextFrame => (Delta > TargetDelta) && !IsClosed;

    private RaylibWindow(string title, int width, int height, long targetFps, Type currentControllerType)
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
        CurrentViewType = currentControllerType;
        RaylibService.InitWindow(width, height, title);
    }

    public static IGameWindow Create(string title, int width, int height, long targetFps, Type currentControllerType)
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

        _instance = new RaylibWindow(title, width, height, targetFps, currentControllerType);
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
        if (IsClosed)
        {
            return true;
        }

        return RaylibService.WindowShouldClose();
    }

    public void Close()
    {
        if(IsClosed)
        {
            return;
        }

        RaylibService.CloseWindow();
        IsClosed = true;
    }

    public void SetTargetFps(long fpsTarget)
    {
        if (fpsTarget < 1)
        {
            throw new WrongOperationException("Target FPS value cant be less than 1.");
        }

        TargetFps = fpsTarget;
        TargetDelta = OneSecond / TargetFps;
    }

    public void NextView<TView>() where TView : IGameView
    {
        CurrentViewType = typeof(TView);
    }

    public void NextView<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel
    {
        CurrentViewType = typeof(TView);
    }

    public void NextView(Type tview)
    {
        if (tview.GetInterface(nameof(IGameView)) is null && tview.GetInterface(typeof(IGameView<>).Name) is null)
        {
            throw new InvalidValueException($"The type ({nameof(tview)}) doesnt implement the {nameof(IGameView)} interface.");
        }

        CurrentViewType = tview;
    }
}
