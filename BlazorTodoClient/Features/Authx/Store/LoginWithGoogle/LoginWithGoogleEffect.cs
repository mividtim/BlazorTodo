using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.ServiceClients;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.LoginWithGoogle;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LoginWithGoogleEffect : Effect<LoginWithGoogleAction>
{
    private readonly BlazorTodoApiClient _apiClient;
    private readonly IAuthxService _authxService;

    public LoginWithGoogleEffect(BlazorTodoApiClient apiClient, IAuthxService authxService) =>
        (_apiClient, _authxService) =
        (apiClient, authxService);

    public override async Task HandleAsync(LoginWithGoogleAction action, IDispatcher dispatcher)
    {
        var authResult = await _apiClient.PostAsync("authx/token/google", action.LoginWithGoogleDto);
        if (!authResult.IsSuccessStatusCode)
        {
            dispatcher.Dispatch(new LoginWithGoogleFailureAction(authResult.ReasonPhrase ?? "Unknown reason"));
            throw new UnauthorizedAccessException();
        }
        await _authxService.CompleteLoginWithAuthResult(authResult);
        dispatcher.Dispatch(new LoginWithGoogleSuccessAction());
    }
}