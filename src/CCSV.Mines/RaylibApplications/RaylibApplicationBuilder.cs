using CCSV.Mines.GameApplications;

namespace CCSV.Mines.RaylibApplications;

public class RaylibApplicationBuilder : IGameApplicationBuilder
{
    public IGameWindowBuilder Window { get; private set; }

    private RaylibApplicationBuilder(IGameWindowBuilder window)
    {
        Window = window;
    }

    public static IGameApplicationBuilder CreateBuilder()
    {
        return new RaylibApplicationBuilder(new RaylibWindowBuilder());
    }

    public IGameApplication Build()
    {
        return RaylibApplication.Create(Window);
    }
}
