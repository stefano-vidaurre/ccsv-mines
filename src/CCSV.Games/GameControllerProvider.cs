using CCSV.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace CCSV.Games;
public class GameControllerProvider : IGameControllerProvider
{

    private readonly IGameControllerViewMatcher _matcher;
    private readonly IServiceProvider _serviceProvider;
    private IServiceScope _serviceScope;

    public GameControllerProvider(IGameControllerViewMatcher matcher, IServiceProvider serviceProvider)
    {
        _matcher = matcher;
        _serviceProvider = serviceProvider;
        _serviceScope = _serviceProvider.CreateScope();
    }

    public IGameController BuildController<TView>() where TView : IGameView
    {
        Type? controllerType = _matcher.GetByView<TView>();

        if (controllerType is null)
        {
            throw new BusinessException($"The view ({nameof(TView)}) has not any controller.");
        }

        _serviceScope.Dispose();
        _serviceScope = _serviceProvider.CreateScope();

        return _serviceScope.ServiceProvider.GetService(controllerType) as IGameController ?? throw new BusinessException($"The controller ({controllerType.Name}) doesnt implement {nameof(IGameController)} interface.");
    }

    public IGameController BuildController<TView, TModel>()
        where TView : IGameView<TModel>
        where TModel : GameViewModel
    {
        Type? controllerType = _matcher.GetByView<TView, TModel>();

        if (controllerType is null)
        {
            throw new BusinessException($"The view ({nameof(TView)}) has not any controller.");
        }

        _serviceScope.Dispose();
        _serviceScope = _serviceProvider.CreateScope();

        return _serviceScope.ServiceProvider.GetService(controllerType) as IGameController ?? throw new BusinessException($"The controller ({controllerType.Name}) doesnt implement {nameof(IGameController)} interface.");
    }

    public IGameController BuildController(Type tview)
    {
        Type? controllerType = _matcher.GetByView(tview);

        if (controllerType is null)
        {
            throw new BusinessException($"The view ({tview.Name}) has not any controller.");
        }

        _serviceScope.Dispose();
        _serviceScope = _serviceProvider.CreateScope();

        return _serviceScope.ServiceProvider.GetService(controllerType) as IGameController ?? throw new BusinessException($"The controller ({controllerType.Name}) doesnt implement {nameof(IGameController)} interface.");
    }
}
