using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public class GameConfiguration : IGameConfiguration
{
    public string Title { get; init; } = "Window title";
    public long TargetFps { get; init; } = 60;
    public int Height { get; init; } = 480;
    public int Width { get; init; } = 800;
    public string Version { get; init; } = "0.0.0";
}
