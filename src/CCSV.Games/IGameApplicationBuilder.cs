using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;

public interface IGameApplicationBuilder
{
    IGameWindowBuilder Window { get; }
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
    IGameEnvironment Environment { get; }

    IGameApplication Build<TMainView>() where TMainView : IGameView;
}
