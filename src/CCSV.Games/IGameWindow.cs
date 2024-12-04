namespace CCSV.Games;

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
    public Type CurrentViewType { get; }

    public bool IsNextFrame { get; }
    public DateTime DrawingSince { get; }
    public long Delta { get; }

    void SetTargetFps(long fpsTarget);
    void NextView<TView>() where TView : IGameView;
    void NextView<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel;
    void NextView(Type tcontroller);

    void BeginDrawing();
    void EndDrawing();
    bool ShouldClose();
    void Close();
}
