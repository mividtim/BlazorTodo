using BlazorTodoClient.Store;

namespace BlazorTodoClient.Features.Authx.Store.Login;

public class LoginFailureAction : FailureAction
{
    public LoginFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}