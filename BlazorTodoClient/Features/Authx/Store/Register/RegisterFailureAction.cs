using BlazorTodoClient.Store;

namespace BlazorTodoClient.Features.Authx.Store.Register;

public class RegisterFailureAction : FailureAction
{
    public RegisterFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}