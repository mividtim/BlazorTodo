using BlazorTodoClient.Store;

namespace BlazorTodoClient.Features.Todos.Store.CreateTodo;

public class CreateTodoFailureAction : FailureAction
{
    public CreateTodoFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}