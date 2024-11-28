using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public interface IGameEventHandler
{
    Task Update();
    void Draw();
}
