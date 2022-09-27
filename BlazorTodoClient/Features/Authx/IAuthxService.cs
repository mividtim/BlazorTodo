using BlazorTodoDtos.Authx;

namespace BlazorTodoClient.Features.Authx;

public interface IAuthxService
{
    Task<UserDto> RegisterUser(CreateUserDto dto);
    Task Login(CreateAuthxTokensDto dto);
    Task Logout();
}