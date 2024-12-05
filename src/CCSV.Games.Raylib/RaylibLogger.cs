using Microsoft.Extensions.Logging;
using RaylibService = Raylib_cs.Raylib;

namespace CCSV.Games.Raylib;
public class RaylibLogger : ILogger
{
    private readonly string _loggerName;

    public RaylibLogger(string loggerName)
    {
        _loggerName = loggerName;
        RaylibService.SetTraceLogLevel(Raylib_cs.TraceLogLevel.All);
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return default!;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        string message = $"[{_loggerName}] - {formatter(state, exception)}";

        switch (logLevel)
        {
            case LogLevel.Debug:
                RaylibService.TraceLog(Raylib_cs.TraceLogLevel.Debug, message);
                break;
            case LogLevel.Trace:
                RaylibService.TraceLog(Raylib_cs.TraceLogLevel.Trace, message);
                break;
            case LogLevel.Warning:
                RaylibService.TraceLog(Raylib_cs.TraceLogLevel.Warning, message);
                break;
            case LogLevel.Error:
            case LogLevel.Critical:
                RaylibService.TraceLog(Raylib_cs.TraceLogLevel.Error, message);
                break;
            case LogLevel.Information:
                RaylibService.TraceLog(Raylib_cs.TraceLogLevel.Info, message);
                break;
            default:
                RaylibService.TraceLog(Raylib_cs.TraceLogLevel.None, message);
                break;
        }
    }
}
