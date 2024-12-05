using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSV.Games.Raylib;

public class RaylibApplicationBuilder : GameApplicationBuilder
{
    private RaylibApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services) : base(windowBuilder, services)
    {
    }

    public static IGameApplicationBuilder CreateBuilder()
    {
        IGameWindowBuilder windowBuilder = new RaylibWindowBuilder();
        IServiceCollection services = new ServiceCollection();
        services.AddLogging(builder => builder.AddRaylibLogger(LogLevel.Information));

        return new RaylibApplicationBuilder(windowBuilder, services);
    }
}
