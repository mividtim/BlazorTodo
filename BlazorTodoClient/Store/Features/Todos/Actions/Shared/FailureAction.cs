namespace BlazorTodoClient.Store.Features.Todos.Actions.Shared;

public abstract class FailureAction
{
    protected FailureAction(string errorMessage) => ErrorMessage = errorMessage;

    public string ErrorMessage { get; }
}