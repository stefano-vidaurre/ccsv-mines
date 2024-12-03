using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;

[AttributeUsage(AttributeTargets.Method)]
public abstract class GameEventAttribute : Attribute
{
    public abstract bool HasHappened();
}
