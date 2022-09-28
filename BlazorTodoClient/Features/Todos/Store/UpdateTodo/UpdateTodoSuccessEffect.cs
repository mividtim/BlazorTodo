using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Navigation.Store.NavigateTo;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.UpdateTodo;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class UpdateTodoSuccessEffect : Effect<UpdateTodoSuccessAction>
{
    private readonly ILogger<UpdateTodoSuccessEffect> _logger;

    public UpdateTodoSuccessEffect(ILogger<UpdateTodoSuccessEffect> logger) => _logger = logger;

    public override Task HandleAsync(UpdateTodoSuccessAction action, IDispatcher dispatcher)
    {
        _logger.LogInformation("Updated todo successfully, navigating back to todo listing...");
        dispatcher.Dispatch(new NavigateToAction("todos"));
        return Task.CompletedTask;
    }
}