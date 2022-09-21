using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Features.Todos.Store.UpdateTodo;

public class UpdateTodoFailureAction : FailureAction
{
    public UpdateTodoFailureAction(string errorMessage) : base(errorMessage)
    {
    } 
}