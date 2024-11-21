using CCSV.Games;
using CCSV.Mines.Views;

namespace CCSV.Mines.Controllers;

public class MainController : GameController
{
    public MainController(IMainView gameView) : base(gameView)
    {
    }
}
