using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Navigation.Store.NavigateTo;
using Fluxor;

namespace BlazorTodoClient.Features.Navigation.Store.NavigateBack;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class NavigateBackEffect : Effect<NavigateBackAction>
{
    private readonly IState<NavigationState> _state;

    public NavigateBackEffect(IState<NavigationState> state) => _state = state;

    public override Task HandleAsync(NavigateBackAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NavigateToAction(_state.Value.PreviousRoute));
        return Task.CompletedTask;
    }
}