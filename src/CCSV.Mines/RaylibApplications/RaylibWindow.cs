using CCSV.Mines.GameApplications;
using Raylib_cs;

namespace CCSV.Mines.RaylibApplications;

public class RaylibWindow : IGameWindow
{
    private static RaylibWindow? _instance = null;

    public static bool IsInitialised => _instance != null;

    public string Title { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    private RaylibWindow()
    {
        Title = "";
        Width = 0;
        Height = 0;
    }

    private RaylibWindow(string title, int width, int height)
    {
        Title = title;
        Width = width;
        Height = height;

        Raylib.InitWindow(width, height, title);
    }

    public static IGameWindow Create(string title, int width, int height)
    {
        if (_instance is not null)
        {
            _instance.Close();
            throw new Exception("The window is already initialised.");
        }

        _instance = new RaylibWindow(title, width, height);
        return _instance;
    }

    public void BeginDrawing()
    {
        Raylib.BeginDrawing();
    }

    public void EndDrawing()
    {
        Raylib.EndDrawing();
    }

    public bool ShouldClose()
    {
        return Raylib.WindowShouldClose();
    }

    public void Close()
    {
        Raylib.CloseWindow();
    }
}
