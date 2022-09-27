using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorTodoClient.Features.Authx;

public class AuthxStateProvider : AuthenticationStateProvider
{
    public const string LocalStorageKeyIdentityToken = "identityToken";
    private const string JwtAuthType = "jwtAuthType";

    private readonly AuthxMessageHandler _authxMessageHandler;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthxStateProvider(AuthxMessageHandler authxMessageHandler, ILocalStorageService localStorage)
    {
        _authxMessageHandler = authxMessageHandler;
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(LocalStorageKeyIdentityToken);
        if (string.IsNullOrWhiteSpace(token))
        {
            _authxMessageHandler.RemoveBearer();
            return _anonymous;
        }
        _authxMessageHandler.AddBearer(token);
        return GetAuthenticationStateFromToken(token);
    }

    public void NotifyUserLoggedIn(string token) =>
        NotifyAuthenticationStateChanged(Task.FromResult(GetAuthenticationStateFromToken(token)));

    public void NotifyUserLoggedOut() =>
        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));

    private static AuthenticationState GetAuthenticationStateFromToken(string token)
    {
        var claimsIdentity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), JwtAuthType);
        var authenticatedUser = new ClaimsPrincipal(claimsIdentity);
        var authState = new AuthenticationState(authenticatedUser);
        return authState;
    }
}