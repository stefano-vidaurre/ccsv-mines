using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Mines.GameApplications;

public interface IGameWindow
{
    public string Title { get; }
    public int Width { get; }
    public int Height { get; }

    bool ShouldClose();
    void Close();
}
