using System.Diagnostics.CodeAnalysis;
using Blazored.LocalStorage;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorTodoClient.Features.Authx.Store.Logout;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class LogoutEffect : Effect<LogoutAction>
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthxStateProvider _authxStateProvider;
    private readonly AuthxMessageHandler _authxMessageHandler;

    public LogoutEffect(
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider,
        AuthxMessageHandler authxMessageHandler
    ) =>
        (_localStorage, _authxStateProvider, _authxMessageHandler) =
        (localStorage, (AuthxStateProvider)authenticationStateProvider, authxMessageHandler);
    
    public override async Task HandleAsync(LogoutAction _, IDispatcher dispatcher)
    {
        await _localStorage.RemoveItemAsync(IAuthxService.LocalStorageKeyIdentityToken);
        _authxStateProvider.NotifyUserLoggedOut();
        _authxMessageHandler.RemoveBearer();
        dispatcher.Dispatch(new LogoutSuccessAction());
    }
}