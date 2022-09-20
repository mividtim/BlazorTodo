using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Store.Features.Todos.Actions.DeleteTodo;

public class DeleteTodoFailureAction : FailureAction
{
    public DeleteTodoFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}