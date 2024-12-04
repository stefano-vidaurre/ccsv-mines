using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public interface IGameControllerViewCollection : IReadOnlyDictionary<Type, Type>
{
    Type? GetByView(Type tview);
    Type? GetByView<TView>();
}
