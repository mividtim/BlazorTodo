namespace BlazorTodoClient.Store;

public abstract class FailureAction
{
    protected FailureAction(string errorMessage) => ErrorMessage = errorMessage;

    public string ErrorMessage { get; }
}