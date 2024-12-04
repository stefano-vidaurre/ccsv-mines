using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public interface IGameControllerViewMatcher
{
    Type? GetByView(Type tview);
    Type? GetByView<TView>() where TView : IGameView;
    Type? GetByView<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel;
}
