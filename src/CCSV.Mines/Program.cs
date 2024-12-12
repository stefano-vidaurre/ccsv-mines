using CCSV.Games;
using CCSV.Games.Raylib;
using CCSV.Mines.Controllers;
using CCSV.Mines.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Mines;

public static class Program
{
    public static void Main()
    {
        IGameApplicationBuilder builder = RaylibApplicationBuilder.CreateBuilder();
        builder.Window
            .SetTitle("Mines")
            .SetSize(640, 480)
            .SetTargetFps(60);

        builder.Services.AddScoped<MainController>();
        builder.Services.AddScoped<OtherController>();
        builder.Services.AddScoped<MainView>();
        builder.Services.AddScoped<OtherView>();

        IGameApplication gameApplication = builder.Build<MainView>();
        gameApplication.Run();
    }
}
