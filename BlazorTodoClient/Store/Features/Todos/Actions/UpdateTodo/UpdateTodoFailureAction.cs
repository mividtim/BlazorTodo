using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Store.Features.Todos.Actions.UpdateTodo;

public class UpdateTodoFailureAction : FailureAction
{
    public UpdateTodoFailureAction(string errorMessage) : base(errorMessage)
    {
    } 
}