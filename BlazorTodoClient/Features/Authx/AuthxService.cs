using System.Net;
using Blazored.LocalStorage;
using BlazorTodoClient.Features.Authx.Store.UserLoginOrLogOut;
using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Authx;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BlazorTodoClient.Features.Authx;

public class AuthxService : IAuthxService
{
    private readonly BlazorTodoApiService _apiService;
    private readonly AuthxMessageHandler _authxMessageHandler;
    private readonly AuthxStateProvider _authxStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly IDispatcher _dispatcher;

    public AuthxService(
        BlazorTodoApiService apiService,
        AuthxMessageHandler authxMessageHandler,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage,
        IDispatcher dispatcher
    ) =>
        (_apiService, _authxMessageHandler, _authxStateProvider, _localStorage, _dispatcher) =
        (apiService, authxMessageHandler, (AuthxStateProvider)authenticationStateProvider, localStorage, dispatcher);

    public async Task<UserDto> CreateUser(CreateUserDto dto)
    {
        var createResult = await _apiService.PostAsync("authx/user", dto);
        if (!createResult.IsSuccessStatusCode)
        {
            if (createResult.StatusCode == HttpStatusCode.BadRequest)
                throw new FormatException();
            throw new ApplicationException();
        }
        var jsonString = await createResult.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserDto>(jsonString);
        return user;
    }

    public async Task Login(CreateAuthxTokensDto dto)
    {
        var authResult = await _apiService.PostAsync("authx/token", dto);
        if (!authResult.IsSuccessStatusCode) throw new UnauthorizedAccessException();
        var jsonString = await authResult.Content.ReadAsStringAsync();
        var authTokens = JsonConvert.DeserializeObject<AuthTokensDto>(jsonString);
        await _localStorage.SetItemAsync(AuthxStateProvider.LocalStorageKeyIdentityToken, authTokens.IdentityToken);
        _authxStateProvider.NotifyUserLoggedIn(authTokens.IdentityToken);
        _authxMessageHandler.AddBearer(authTokens.IdentityToken);
        _dispatcher.Dispatch(new UserLoginOrLogOutAction());
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(AuthxStateProvider.LocalStorageKeyIdentityToken);
        _authxStateProvider.NotifyUserLoggedOut();
        _authxMessageHandler.RemoveBearer();
        _dispatcher.Dispatch(new UserLoginOrLogOutAction());
    }
}