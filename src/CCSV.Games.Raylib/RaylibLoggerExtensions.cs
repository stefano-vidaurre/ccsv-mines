using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CCSV.Games.Raylib;
public static class RaylibLoggerExtensions
{
    public static ILoggingBuilder AddRaylibLogger(this ILoggingBuilder builder, LogLevel minimumLevel)
    {
        builder.SetMinimumLevel(minimumLevel);
        builder.Services.AddSingleton<ILoggerProvider, RaylibLoggerProvider>();

        return builder;
    }
}
