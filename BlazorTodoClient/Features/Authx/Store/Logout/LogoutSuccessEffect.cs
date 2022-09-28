using System.Diagnostics.CodeAnalysis;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Features.Authx.Store.Logout;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LogoutSuccessEffect : Effect<LogoutSuccessAction>
{
    private readonly NavigationManager _navigationManager;

    public LogoutSuccessEffect(NavigationManager navigationManager) => _navigationManager = navigationManager;

    public override Task HandleAsync(LogoutSuccessAction action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo("/");
        return Task.CompletedTask;
    }
}