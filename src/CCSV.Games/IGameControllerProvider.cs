using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public interface IGameControllerProvider
{
    IGameController BuildController<TView>() where TView : IGameView;
    IGameController BuildController<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel;
    IGameController BuildController(Type tview);
}
