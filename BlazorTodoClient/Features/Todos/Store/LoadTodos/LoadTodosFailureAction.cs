using BlazorTodoClient.Store;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodos;

public class LoadTodosFailureAction : FailureAction
{
    public LoadTodosFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}