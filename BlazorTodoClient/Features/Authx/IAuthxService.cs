using BlazorTodoDtos.Authx;

namespace BlazorTodoClient.Features.Authx;

public interface IAuthxService
{
    Task<UserDto> CreateUser(CreateUserDto dto);
    Task Login(CreateAuthxTokensDto dto);
    Task Logout();
}