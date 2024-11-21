using CCSV.Games;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Mines.RaylibApplications;

public class RaylibApplicationBuilder : GameApplicationBuilder
{
    private RaylibApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services) : base(windowBuilder, services)
    {
    }

    public static IGameApplicationBuilder CreateBuilder()
    {
        IGameWindowBuilder windowBuilder = new RaylibWindowBuilder();
        IServiceCollection services = new ServiceCollection();

        return new RaylibApplicationBuilder(windowBuilder, services);
    }
}
