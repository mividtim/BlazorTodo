using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodos;

public class LoadTodosFailureAction : FailureAction
{
    public LoadTodosFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}