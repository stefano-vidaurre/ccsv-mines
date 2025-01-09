namespace CCSV.Games;
public class GameEnvironment : IGameEnvironment
{
    public string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

    public string GameRootPath => Environment.CurrentDirectory;

    public Version NetVersion => Environment.Version;

    public bool IsDevelopment => EnvironmentName.Equals("Development");

    public bool IsProduction => EnvironmentName.Equals("Production");

    public bool IsStaging => EnvironmentName.Equals("Staging");

    public bool IsEnvironment(string environmentName)
    {
        return EnvironmentName.Equals(environmentName);
    }
}
