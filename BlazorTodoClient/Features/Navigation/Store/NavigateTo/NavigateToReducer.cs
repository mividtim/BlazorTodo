using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Navigation.Store.NavigateTo;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class NavigateToReducer
{
    [ReducerMethod]
    public static NavigationState ReduceNavigateTo(NavigationState state, NavigateToAction action) =>
        action.Route == state.CurrentRoute
            ? state
            : new NavigationState(action.Route, state.CurrentRoute);
}