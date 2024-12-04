using CCSV.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public class GameControllerProvider : IGameControllerProvider
{
    private readonly IGameControllerViewMatcher _matcher;
    private readonly IServiceProvider _serviceProvider;

    public GameControllerProvider(IGameControllerViewMatcher matcher, IServiceProvider serviceProvider)
    {
        _matcher = matcher;
        _serviceProvider = serviceProvider;
    }

    public IGameController GetGameController<TView>() where TView : IGameView
    {
        Type? controllerType = _matcher.GetByView<TView>();

        if (controllerType is null)
        {
            throw new BusinessException($"The view ({nameof(TView)}) has not any controller.");
        }

        return _serviceProvider.GetService(controllerType) as IGameController ?? throw new BusinessException($"The controller ({controllerType.Name}) doesnt implement {nameof(IGameController)} interface.");
    }

    public IGameController GetGameController<TView, TModel>()
        where TView : IGameView<TModel>
        where TModel : GameViewModel
    {
        Type? controllerType = _matcher.GetByView<TView, TModel>();

        if (controllerType is null)
        {
            throw new BusinessException($"The view ({nameof(TView)}) has not any controller.");
        }

        return _serviceProvider.GetService(controllerType) as IGameController ?? throw new BusinessException($"The controller ({controllerType.Name}) doesnt implement {nameof(IGameController)} interface.");
    }

    public IGameController GetGameController(Type tview)
    {
        Type? controllerType = _matcher.GetByView(tview);

        if (controllerType is null)
        {
            throw new BusinessException($"The view ({tview.Name}) has not any controller.");
        }

        return _serviceProvider.GetService(controllerType) as IGameController ?? throw new BusinessException($"The controller ({controllerType.Name}) doesnt implement {nameof(IGameController)} interface.");
    }
}
