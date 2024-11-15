﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Mines.GameApplications;

public interface IGameApplicationBuilder
{
    IGameWindowBuilder Window { get; }

    IGameApplication Build();
}
