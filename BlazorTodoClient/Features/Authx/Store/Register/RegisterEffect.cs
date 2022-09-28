using System.Diagnostics.CodeAnalysis;
using System.Net;
using Blazored.LocalStorage;
using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Authx;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BlazorTodoClient.Features.Authx.Store.Register;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class RegisterEffect : Effect<RegisterAction>
{
    private readonly BlazorTodoApiService _apiService;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthxStateProvider _authxStateProvider;
    private readonly AuthxMessageHandler _authxMessageHandler;

    public RegisterEffect(
        BlazorTodoApiService apiService,
        AuthxMessageHandler authxMessageHandler,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage
    ) =>
        (_apiService, _authxMessageHandler, _authxStateProvider, _localStorage) =
        (apiService, authxMessageHandler, (AuthxStateProvider)authenticationStateProvider, localStorage);

    public override async Task HandleAsync(RegisterAction action, IDispatcher dispatcher)
    {
        var createResult = await _apiService.PostAsync("authx/user", action.CreateUserDto);
        if (!createResult.IsSuccessStatusCode)
        {
            if (createResult.StatusCode == HttpStatusCode.BadRequest)
                throw new FormatException();
            throw new ApplicationException();
        }
        var jsonString = await createResult.Content.ReadAsStringAsync();
        var authTokens = JsonConvert.DeserializeObject<AuthTokensDto>(jsonString);
        await _localStorage.SetItemAsync(IAuthxService.LocalStorageKeyIdentityToken, authTokens.IdentityToken);
        _authxStateProvider.NotifyUserLoggedIn(authTokens.IdentityToken);
        _authxMessageHandler.AddBearer(authTokens.IdentityToken);
        dispatcher.Dispatch(new RegisterSuccessAction());
    }
}