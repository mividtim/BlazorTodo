using Blazored.LocalStorage;
using BlazorTodoClient.Features.Authx.Store.Login;
using BlazorTodoClient.Features.Authx.Store.LoginWithGoogle;
using BlazorTodoClient.Features.Authx.Store.Logout;
using BlazorTodoClient.Features.Authx.Store.Register;
using BlazorTodoDtos.Authx;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BlazorTodoClient.Features.Authx.Store;

public class AuthxService : IAuthxService
{
    private readonly ILogger<AuthxService> _logger;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthxStateProvider _authxStateProvider;
    private readonly AuthxMessageHandler _authxMessageHandler;
    private readonly IDispatcher _dispatcher;

    public AuthxService(
        ILogger<AuthxService> logger,
        AuthxMessageHandler authxMessageHandler,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage,
        IDispatcher dispatcher
    ) =>
        (_logger, _authxMessageHandler, _authxStateProvider, _localStorage, _dispatcher) =
        (logger, authxMessageHandler, (AuthxStateProvider)authenticationStateProvider, localStorage, dispatcher);

    public void Register(CreateUserDto dto)
    {
        _logger.LogInformation("Issuing action to register a new user");
        _dispatcher.Dispatch(new RegisterAction(dto));
    }

    public void Login(CreateAuthxTokensDto dto)
    {
        _logger.LogInformation("Issuing action to login");
        _dispatcher.Dispatch(new LoginAction(dto));
    }

    public void LoginWithGoogle(string credential)
    {
        _logger.LogInformation("Issuing action to login with Google");
        _dispatcher.Dispatch(new LoginWithGoogleAction(new LoginWithGoogleDto { Credential = credential }));
    }

    public async Task CompleteLoginWithAuthResult(HttpResponseMessage authResult)
    {
        var jsonString = await authResult.Content.ReadAsStringAsync();
        var authTokens = JsonConvert.DeserializeObject<AuthTokensDto>(jsonString);
        await _localStorage.SetItemAsync(IAuthxService.LocalStorageKeyIdentityToken, authTokens.IdentityToken);
        _authxStateProvider.NotifyUserLoggedIn(authTokens.IdentityToken);
        _authxMessageHandler.AddBearer(authTokens.IdentityToken);
    }

    public void Logout()
    {
        _logger.LogInformation("Issuing action to logout");
        _dispatcher.Dispatch(new LogoutAction());
    }
}