namespace CCSV.Games;

public interface IGameWindowBuilder
{
    Type? MainView { get; }

    IGameWindowBuilder SetTitle(string title);
    IGameWindowBuilder SetSize(int width, int height);
    IGameWindowBuilder SetTargetFps(long targetFps);
    IGameWindowBuilder SetMainView<TView>() where TView : IGameView;
    IGameWindowBuilder SetMainView(Type tview);
    IGameWindow Build();
}
