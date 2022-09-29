using BlazorTodoDtos.Authx;

namespace BlazorTodoClient.Features.Authx.Store.LoginWithGoogle;

public class LoginWithGoogleAction
{
    public LoginWithGoogleAction(LoginWithGoogleDto loginWithGoogleDto) => LoginWithGoogleDto = loginWithGoogleDto;

    public LoginWithGoogleDto LoginWithGoogleDto { get; }
}