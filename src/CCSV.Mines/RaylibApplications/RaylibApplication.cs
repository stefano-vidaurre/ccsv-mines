using CCSV.Mines.GameApplications;
using Raylib_cs;

namespace CCSV.Mines.RaylibApplications;

public class RaylibApplication : IGameApplication
{
    private readonly IGameWindow _window;

    private RaylibApplication(IGameWindowBuilder builder)
    {
        _window = builder.Build();
    }

    public static RaylibApplication Create(IGameWindowBuilder builder)
    {
        return new RaylibApplication(builder);
    }

    public void Run()
    {
        while (!_window.ShouldClose())
        {
            Update();
            Draw();
        }

        _window.Close();
    }

    private void Update()
    {
        //
    }

    private void Draw()
    {
        _window.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
        _window.EndDrawing();
    }
}
