﻿using CCSV.Games;
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

        builder.Controllers.AddGameController<MainController>();
        builder.Controllers.AddGameController<OtherController>();

        builder.Services.AddScoped<IMainView, MainView>();
        builder.Services.AddScoped<IOtherView, OtherView>();

        IGameApplication gameApplication = builder.Build();
        gameApplication.Run();
    }
}
