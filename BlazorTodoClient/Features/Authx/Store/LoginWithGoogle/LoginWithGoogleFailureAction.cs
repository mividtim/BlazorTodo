using BlazorTodoClient.Store;

namespace BlazorTodoClient.Features.Authx.Store.LoginWithGoogle;

public class LoginWithGoogleFailureAction : FailureAction
{
    public LoginWithGoogleFailureAction(string errorMessage) : base(errorMessage)
    {
    }
}