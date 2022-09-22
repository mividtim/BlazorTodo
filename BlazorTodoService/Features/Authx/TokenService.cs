using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BlazorTodoService.Features.Authx;

public class TokenService : ITokenService
{
    private const double ExpiryDurationMinutes = 30d;

    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;
    private readonly SymmetricSecurityKey _jwtSecurityKey;

    public TokenService(string jwtIssuer, string jwtAudience, SymmetricSecurityKey jwtSecurityKey) =>
        (_jwtIssuer, _jwtAudience, _jwtSecurityKey) = (jwtIssuer, jwtAudience, jwtSecurityKey);

    public string CreateToken(AuthxUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, string.Join(";", roles))
        };
        var credentials = new SigningCredentials(_jwtSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(ExpiryDurationMinutes),
            Issuer = _jwtIssuer,
            Audience = _jwtAudience,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token)!;
        return jwtToken;
    }
}