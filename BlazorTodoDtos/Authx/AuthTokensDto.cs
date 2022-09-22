namespace BlazorTodoDtos.Authx;

public class AuthTokensDto
{
    public string IdentityToken { get; set; }

    public AuthTokensDto(string identityToken) => IdentityToken = identityToken;
}