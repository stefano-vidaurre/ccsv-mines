namespace CCSV.Games;
public interface IGameEnvironment
{
    string EnvironmentName { get; }
    string GameRootPath { get; }
    Version NetVersion { get; }
    bool IsDevelopment { get; }
    bool IsProduction { get; }
    bool IsStaging { get; }
    bool IsEnvironment(string environmentName);
}
