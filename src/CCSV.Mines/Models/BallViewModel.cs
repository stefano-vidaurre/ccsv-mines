using CCSV.Games;

namespace CCSV.Mines.Models;
public class BallViewModel : GameViewModel
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int Radius { get; set; }
}
