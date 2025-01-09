using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSV.Games.Raylib;

public class RaylibApplicationBuilder : GameApplicationBuilder
{
    private RaylibApplicationBuilder(IGameWindowBuilder windowBuilder, IServiceCollection services, IConfiguration configuration, IGameEnvironment environment) : base(windowBuilder, services, configuration, environment)
    {
    }

    public static IGameApplicationBuilder CreateBuilder()
    {
        IGameEnvironment environment = new GameEnvironment();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true).Build();
        IGameConfiguration windowConfiguration = configuration.GetSection(nameof(GameConfiguration)).Get<GameConfiguration>() ?? new GameConfiguration();

        IGameWindowBuilder windowBuilder = new RaylibWindowBuilder(windowConfiguration);
        IServiceCollection services = new ServiceCollection();
        services.AddLogging(builder => builder.AddRaylibLogger(LogLevel.Information));
        services.AddSingleton<IGameConfiguration>(windowConfiguration);

        return new RaylibApplicationBuilder(windowBuilder, services, configuration, environment);
    }
}
