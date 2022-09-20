using BlazorTodoClient.Store.Features.Todos.Actions.UpdateTodo;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Store.Features.Todos.Effects;

public class UpdateTodoSuccessEffect : Effect<UpdateTodoSuccessAction>
{
    private readonly ILogger<UpdateTodoSuccessEffect> _logger;
    private readonly NavigationManager _navigation;

    public UpdateTodoSuccessEffect(ILogger<UpdateTodoSuccessEffect> logger, NavigationManager navigation) =>
        (_logger, _navigation) = (logger, navigation);

    public override Task HandleAsync(UpdateTodoSuccessAction action, IDispatcher dispatcher)
    {
        _logger.LogInformation("Updated todo successfully, navigating back to todo listing...");
        _navigation.NavigateTo("todos");
        return Task.CompletedTask;
    }
}