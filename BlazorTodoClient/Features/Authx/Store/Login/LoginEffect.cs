using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.ServiceClients;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.Login;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LoginEffect : Effect<LoginAction>
{
    private readonly BlazorTodoApiClient _apiClient;
    private readonly IAuthxService _authxService;

    public LoginEffect(BlazorTodoApiClient apiClient, IAuthxService authxService) =>
        (_apiClient, _authxService) =
        (apiClient, authxService);

    public override async Task HandleAsync(LoginAction action, IDispatcher dispatcher)
    {
        var authResult = await _apiClient.PostAsync("authx/token", action.CreateAuthxTokensDto);
        if (!authResult.IsSuccessStatusCode)
        {
            dispatcher.Dispatch(new LoginFailureAction(authResult.ReasonPhrase ?? "Unknown reason"));
            throw new UnauthorizedAccessException();
        }
        await _authxService.CompleteLoginWithAuthResult(authResult);
        dispatcher.Dispatch(new LoginSuccessAction());
    }
}