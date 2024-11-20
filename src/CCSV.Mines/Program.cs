using CCSV.Mines.Controllers;
using CCSV.Mines.GameApplications;
using CCSV.Mines.RaylibApplications;
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
            .SetSize(400, 400);

        builder.Services.AddSingleton<IGameController, MainController>();
        builder.Services.AddSingleton<IMainView, MainView>();

        IGameApplication gameApplication = builder.Build();
        gameApplication.Run();
    }
}
