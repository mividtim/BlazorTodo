using System.Diagnostics.CodeAnalysis;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Features.Authx.Store.Login;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LoginSuccessEffect : Effect<LoginSuccessAction>
{
    private readonly NavigationManager _navigationManager;

    public LoginSuccessEffect(NavigationManager navigationManager) =>
        _navigationManager = navigationManager;

    public override Task HandleAsync(LoginSuccessAction action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo("/");
        return Task.CompletedTask;
    }
}