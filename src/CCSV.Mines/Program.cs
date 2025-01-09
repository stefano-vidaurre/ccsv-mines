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

        builder.Services.AddSingleton<MainController>();
        builder.Services.AddScoped<MainView>();
        builder.Services.AddScoped<TitleView>();
        builder.Services.AddScoped<OtherView>();

        IGameApplication gameApplication = builder.Build<TitleView>();
        gameApplication.Run();
    }
}
