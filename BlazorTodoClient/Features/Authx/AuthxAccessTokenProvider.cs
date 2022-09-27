using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTodoClient.Features.Authx;

public class AuthxAccessTokenProvider : IAccessTokenProvider
{
    public ValueTask<AccessTokenResult> RequestAccessToken()
    {
        return new ValueTask<AccessTokenResult>(new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(60),
            GrantedScopes = new[] { "public" },
            Value = "asdf"
        }, "/login"));
    }

    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
    {
        return new ValueTask<AccessTokenResult>(new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken
        {
            Expires = DateTimeOffset.UtcNow.AddMinutes(60),
            GrantedScopes = new[] { "public" },
            Value = "asdf"
        }, "/login"));
    }
}