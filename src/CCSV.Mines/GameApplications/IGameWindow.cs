namespace CCSV.Mines.GameApplications;

public interface IGameWindow
{
    public string Title { get; }
    public int Width { get; }
    public int Height { get; }
    public long TargetFps { get; }
    public long Fps { get; }
    public long TargetDelta { get; }
    public long LastDelta { get; }
    public bool IsClosed { get; }
    public bool IsDrawing { get; }

    public bool IsNextFrame { get; }
    public DateTime DrawingSince { get; }
    public long Delta { get; }


    void BeginDrawing();
    void EndDrawing();
    bool ShouldClose();
    void Close();
}
