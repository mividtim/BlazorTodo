using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;

public class LoadTodoDetailFailureAction : FailureAction
{
    public LoadTodoDetailFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}