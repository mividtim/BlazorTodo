using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Navigation.Store.NavigateTo;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.Logout;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LogoutSuccessEffect : Effect<LogoutSuccessAction>
{
    public override Task HandleAsync(LogoutSuccessAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NavigateToAction("/"));
        return Task.CompletedTask;
    }
}