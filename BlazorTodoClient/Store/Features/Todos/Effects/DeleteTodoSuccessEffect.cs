using BlazorTodoClient.Store.Features.Todos.Actions.DeleteTodo;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Store.Features.Todos.Effects;

public class DeleteTodoSuccessEffect : Effect<DeleteTodoSuccessAction>
{
    private readonly ILogger<DeleteTodoSuccessEffect> _logger;
    private readonly NavigationManager _navigation;

    public DeleteTodoSuccessEffect(ILogger<DeleteTodoSuccessEffect> logger, NavigationManager navigation) =>
        (_logger, _navigation) = (logger, navigation);

    public override Task HandleAsync(DeleteTodoSuccessAction action, IDispatcher dispatcher)
    {
        _logger.LogInformation("Deleted todo successfully, navigating back to todo listing...");
        _navigation.NavigateTo("todos");
        return Task.CompletedTask;
    }
}