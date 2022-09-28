using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Navigation.Store.NavigateBack;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.Login;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LoginSuccessEffect : Effect<LoginSuccessAction>
{
    public override Task HandleAsync(LoginSuccessAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NavigateBackAction());
        return Task.CompletedTask;
    }
}