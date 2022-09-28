using System.Diagnostics.CodeAnalysis;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Features.Authx.Store.Register;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class RegisterSuccessEffect : Effect<RegisterSuccessAction>
{
    private readonly NavigationManager _navigationManager;

    public RegisterSuccessEffect(NavigationManager navigationManager) =>
        _navigationManager = navigationManager;

    public override Task HandleAsync(RegisterSuccessAction action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo("/");
        return Task.CompletedTask;
    }
}