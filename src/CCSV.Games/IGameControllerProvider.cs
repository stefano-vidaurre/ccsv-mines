using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public interface IGameControllerProvider
{
    IGameController GetGameController<TView>() where TView : IGameView;
    IGameController GetGameController<TView, TModel>() where TView : IGameView<TModel> where TModel : GameViewModel;
    IGameController GetGameController(Type tview);
}
