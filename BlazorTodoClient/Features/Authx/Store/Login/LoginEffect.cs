using System.Diagnostics.CodeAnalysis;
using Blazored.LocalStorage;
using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Authx;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BlazorTodoClient.Features.Authx.Store.Login;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LoginEffect : Effect<LoginAction>
{
    private readonly BlazorTodoApiService _apiService;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthxStateProvider _authxStateProvider;
    private readonly AuthxMessageHandler _authxMessageHandler;

    public LoginEffect(
        BlazorTodoApiService apiService,
        AuthxMessageHandler authxMessageHandler,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage
    ) =>
        (_apiService, _authxMessageHandler, _authxStateProvider, _localStorage) =
        (apiService, authxMessageHandler, (AuthxStateProvider)authenticationStateProvider, localStorage);

    public override async Task HandleAsync(LoginAction action, IDispatcher dispatcher)
    {
        var authResult = await _apiService.PostAsync("authx/token", action.CreateAuthxTokensDto);
        if (!authResult.IsSuccessStatusCode)
        {
            dispatcher.Dispatch(new LoginFailureAction(authResult.ReasonPhrase));
            throw new UnauthorizedAccessException();
        }
        var jsonString = await authResult.Content.ReadAsStringAsync();
        var authTokens = JsonConvert.DeserializeObject<AuthTokensDto>(jsonString);
        await _localStorage.SetItemAsync(IAuthxService.LocalStorageKeyIdentityToken, authTokens.IdentityToken);
        _authxStateProvider.NotifyUserLoggedIn(authTokens.IdentityToken);
        _authxMessageHandler.AddBearer(authTokens.IdentityToken);
        dispatcher.Dispatch(new LoginSuccessAction());
    }
}