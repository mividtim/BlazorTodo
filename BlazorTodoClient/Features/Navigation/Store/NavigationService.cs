using BlazorTodoClient.Features.Navigation.Store.NavigateTo;
using Fluxor;

namespace BlazorTodoClient.Features.Navigation.Store;

public class NavigationService : INavigationService
{
    private readonly IDispatcher _dispatcher;

    public NavigationService(IDispatcher dispatcher) => _dispatcher = dispatcher;

    public void NavigateTo(string route) => _dispatcher.Dispatch(new NavigateToAction(route));
}