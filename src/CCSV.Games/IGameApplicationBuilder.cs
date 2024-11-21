using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public interface IGameApplicationBuilder
{
    IGameWindowBuilder Window { get; }
    IServiceCollection Services { get; }

    IGameApplication Build();
}
