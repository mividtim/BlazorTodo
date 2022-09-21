using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Features.Todos.Store.DeleteTodo;

public class DeleteTodoFailureAction : FailureAction
{
    public DeleteTodoFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}