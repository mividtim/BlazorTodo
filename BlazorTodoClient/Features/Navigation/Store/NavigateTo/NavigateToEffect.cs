using System.Diagnostics.CodeAnalysis;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Features.Navigation.Store.NavigateTo;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class NavigateToEffect : Effect<NavigateToAction>
{
    private readonly NavigationManager _navigationManager;

    public NavigateToEffect(NavigationManager navigationManager) =>
        _navigationManager = navigationManager;

    public override Task HandleAsync(NavigateToAction action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo(action.Route);
        return Task.CompletedTask;
    }
}