﻿using CCSV.Games;

namespace CCSV.Mines.Models;
public class BallViewModel : GameViewModel
{
    public int PosX { get; init; }
    public int PosY { get; init; }
    public int Radius { get; init; }
}
