using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace CCSV.Games.Raylib;

public class RaylibLoggerProvider : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, RaylibLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new RaylibLogger(name));
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
}
