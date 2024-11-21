using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Mines.GameApplications;

public interface IGameApplicationBuilder
{
    IGameWindowBuilder Window { get; }
    IServiceCollection Services { get; }

    IGameApplication Build();
}
