using BlazorTodoDtos.Authx;
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
    private readonly SignInManager<AuthxUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthxController(
        ILogger<AuthxController> logger,
        UserManager<AuthxUser> userManager,
        SignInManager<AuthxUser> signInManager,
        ITokenService tokenService
    ) => (_logger, _userManager, _signInManager, _tokenService) =
        (logger, userManager, signInManager, tokenService);

    [HttpPost("user")]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
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
        return newUser.ToDto();
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
        var roles = await _userManager.GetRolesAsync(user);
        var identityToken = _tokenService.CreateToken(user, roles);
        return new AuthTokensDto(identityToken);
    }
}