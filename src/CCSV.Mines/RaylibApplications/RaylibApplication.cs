using CCSV.Mines.GameApplications;
using Microsoft.Extensions.DependencyInjection;
using Raylib_cs;

namespace CCSV.Mines.RaylibApplications;

public class RaylibApplication : IGameApplication
{
    private readonly IGameWindow _window;
    private readonly IServiceProvider _services;

    private RaylibApplication(IGameWindow window, IServiceProvider services)
    {
        _window = window;
        _services = services;
    }

    public static RaylibApplication Create(IGameWindowBuilder windowBuilder, IServiceCollection servicesBuilder)
    {
        IGameWindow window = windowBuilder.Build();
        IServiceProvider services = servicesBuilder.BuildServiceProvider();

        return new RaylibApplication(window, services);
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
