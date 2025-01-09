namespace CCSV.Games;

public interface IGameConfiguration
{
    int Height { get; }
    long TargetFps { get; }
    string Title { get; }
    string Version { get; }
    int Width { get; }
}