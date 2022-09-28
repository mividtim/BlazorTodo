using BlazorTodoClient.Features.Navigation.Store.NavigateTo;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.DeleteTodo;

public class DeleteTodoSuccessEffect : Effect<DeleteTodoSuccessAction>
{
    private readonly ILogger<DeleteTodoSuccessEffect> _logger;

    public DeleteTodoSuccessEffect(ILogger<DeleteTodoSuccessEffect> logger) => _logger = logger;

    public override Task HandleAsync(DeleteTodoSuccessAction action, IDispatcher dispatcher)
    {
        _logger.LogInformation("Deleted todo successfully, navigating back to todo listing...");
        dispatcher.Dispatch(new NavigateToAction("todos"));
        return Task.CompletedTask;
    }
}