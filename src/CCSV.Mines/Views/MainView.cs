using CCSV.Mines.RaylibApplications;
using Raylib_cs;

namespace CCSV.Mines.Views;
public class MainView : RaylibView, IMainView
{
    public MainView(RaylibWindow window) : base(window)
    {
    }

    public override void DrawView()
    {
        Raylib.ClearBackground(Color.White);
        Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
    }
}
