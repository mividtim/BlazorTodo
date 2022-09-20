using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Store.Features.Todos.Actions.CreateTodo;

public class CreateTodoFailureAction : FailureAction
{
    public CreateTodoFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}