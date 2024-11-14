using CCSV.Mines.GameApplications;
using CCSV.Mines.RaylibApplications;
using Raylib_cs;

namespace CCSV.Mines;

public static class Program
{
    public static void Main()
    {
        IGameApplicationBuilder builder = RaylibApplicationBuilder.CreateBuilder();
        builder.Window
            .SetTitle("Mines")
            .SetSize(400, 400);

        IGameApplication gameApplication = builder.Build();
        gameApplication.Run();
    }
}
