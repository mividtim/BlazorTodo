using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Navigation.Store.NavigateBack;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.Register;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class RegisterSuccessEffect : Effect<RegisterSuccessAction>
{
    public override Task HandleAsync(RegisterSuccessAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new NavigateBackAction());
        return Task.CompletedTask;
    }
}