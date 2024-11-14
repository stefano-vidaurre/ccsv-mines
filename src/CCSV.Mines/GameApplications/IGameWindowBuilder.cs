using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Mines.GameApplications;

public interface IGameWindowBuilder
{
    IGameWindowBuilder SetTitle(string title);
    IGameWindowBuilder SetSize(int width, int height);
    IGameWindow Build();
}
