using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorTodoDtos.Authx;
using BlazorTodoUtils;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTodoService.Features.Authx;

[Route("api/authx")]
[ApiController]
[AllowAnonymous]
public class AuthxController : ControllerBase
{
    private readonly ILogger<AuthxController> _logger;
    private readonly UserManager<AuthxUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthxController(
        ILogger<AuthxController> logger,
        UserManager<AuthxUser> userManager,
        ITokenService tokenService
    ) => (_logger, _userManager, _tokenService) =
        (logger, userManager, tokenService);

    [HttpPost("user")]
    public async Task<ActionResult<AuthTokensDto>> CreateUser(CreateUserDto dto)
    {
        var normalized = CreateUserDto.Normalize(dto);
        var createUserResult = await _userManager.CreateAsync(
            new AuthxUser
            {
                UserName = normalized.Email,
                GivenName = normalized.GivenName,
                Surname = normalized.Surname,
                NameOrder = normalized.NameOrder ?? ENameOrder.GivenNameFirst,
                PreferredName = normalized.PreferredName,
                Email = normalized.Email,
                PhoneNumber = normalized.PhoneNumber
            },
            normalized.Password);
        if (!createUserResult.Succeeded)
            return Problem(string.Join("; ", createUserResult.Errors.Select(error => error.Description)));
        var newUser = await _userManager.FindByNameAsync(normalized.Email);
        if (newUser is null) return Problem("User not found immediately after creation");
        await _userManager.AddToRoleAsync(newUser, AuthxRoleConfiguration.VisitorRole);
        var authTokensDto = await CreateAuthTokenForUser(newUser);
        return authTokensDto;
    }

    [HttpPost("token")]
    public async Task<ActionResult<AuthTokensDto>> CreateToken(CreateAuthxTokensDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName.Trim());
        if (user is null)
        {
            _logger.LogInformation("User with username {UserName} not found", dto.UserName);
            return Unauthorized();
        }
        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
        {
            _logger.LogInformation("Incorrect password for user with userName {UserName}", dto.UserName);
            return Unauthorized();
        }
        var authTokensDto = await CreateAuthTokenForUser(user);
        return authTokensDto;
    }

    [HttpPost("token/google")]
    public async Task<ActionResult<AuthTokensDto>> LoginWithGoogle(LoginWithGoogleDto dto)
    {
        try
        {
            await GoogleJsonWebSignature.ValidateAsync(dto.Credential);
        }
        catch (InvalidJwtException)
        {
            _logger.LogInformation("JWT presented by client is not a valid JWT signed by Google");
            return Unauthorized();
        }
        var claims = JwtParser.ParseClaimsFromJwt(dto.Credential).ToList();
        string email;
        try
        {
            var emailClaim = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)
                    ?? throw new UnauthorizedAccessException();
            email = emailClaim.Value;
        }
        catch (UnauthorizedAccessException)
        {
            _logger.LogInformation("No email claim found in JWT");
            return Unauthorized();
        }
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            _logger.LogInformation("User with email {Email} not found", email);
            return await RegisterWithGoogle(email, claims);
        }
        var authTokensDto = await CreateAuthTokenForUser(user);
        return authTokensDto;
    }

    private async Task<ActionResult<AuthTokensDto>> RegisterWithGoogle(string email, List<Claim> claims)
    {
        var name = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Name)?.Value;
        var createUserResult = await _userManager.CreateAsync(
            new AuthxUser
            {
                UserName = email,
                PreferredName = name,
                Email = email
            },
            $"A{Guid.NewGuid().ToString()}");
        if (!createUserResult.Succeeded)
            return Problem(string.Join("; ", createUserResult.Errors.Select(error => error.Description)));
        var newUser = await _userManager.FindByEmailAsync(email);
        if (newUser is null) return Problem("User not found immediately after creation");
        await _userManager.AddToRoleAsync(newUser, AuthxRoleConfiguration.VisitorRole);
        var authTokensDto = await CreateAuthTokenForUser(newUser);
        return authTokensDto;
    }

    private async Task<AuthTokensDto> CreateAuthTokenForUser(AuthxUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var identityToken = _tokenService.CreateToken(user, roles);
        var dto = new AuthTokensDto { IdentityToken = identityToken };
        return dto;
    }
}