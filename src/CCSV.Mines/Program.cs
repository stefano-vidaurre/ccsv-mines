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
            .SetSize(400, 400)
            .SetTargetFps(60);

        builder.Controllers.AddGameController<IMainController, MainController>();
        builder.Controllers.SetMain<IMainController>();
        builder.Services.AddSingleton<IMainView, MainView>();

        IGameApplication gameApplication = builder.Build();
        gameApplication.Run();
    }
}
