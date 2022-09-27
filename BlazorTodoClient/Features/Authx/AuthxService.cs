using Blazored.LocalStorage;
using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Authx;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BlazorTodoClient.Features.Authx;

public class AuthxService : IAuthxService
{
    private readonly BlazorTodoApiService _apiService;
    private readonly AuthxMessageHandler _authxMessageHandler;
    private readonly AuthxStateProvider _authxStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthxService(
        BlazorTodoApiService apiService,
        AuthxMessageHandler authxMessageHandler,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage
    ) =>
        (_apiService, _authxMessageHandler, _authxStateProvider, _localStorage) =
        (apiService, authxMessageHandler, (AuthxStateProvider)authenticationStateProvider, localStorage);

    public Task<UserDto> RegisterUser(CreateUserDto dto)
    {
        throw new NotImplementedException();
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
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(AuthxStateProvider.LocalStorageKeyIdentityToken);
        _authxStateProvider.NotifyUserLoggedOut();
        _authxMessageHandler.RemoveBearer();
    }
}