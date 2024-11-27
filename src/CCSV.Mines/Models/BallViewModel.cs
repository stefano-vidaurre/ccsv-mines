using CCSV.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Mines.Models;
public class BallViewModel : GameViewModel
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int Radius { get; set; }
}
