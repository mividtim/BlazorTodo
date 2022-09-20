using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Store.Features.Todos.Actions.LoadTodos;

public class LoadTodosFailureAction : FailureAction
{
    public LoadTodosFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}