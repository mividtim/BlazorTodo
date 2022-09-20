using BlazorTodoClient.Store.Features.Todos.Actions.Shared;

namespace BlazorTodoClient.Store.Features.Todos.Actions.LoadTodoDetail;

public class LoadTodoDetailFailureAction : FailureAction
{
    public LoadTodoDetailFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}