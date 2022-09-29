using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Navigation.Store.NavigateBack;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.LoginWithGoogle;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LoginWithGoogleSuccessEffect : Effect<LoginWithGoogleSuccessAction>
{
    public override Task HandleAsync(LoginWithGoogleSuccessAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NavigateBackAction());
        return Task.CompletedTask;
    }
}