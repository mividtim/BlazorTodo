using BlazorTodoDtos.Authx;

namespace BlazorTodoService.Features.Authx;

public interface ITokenService
{
    public string CreateToken(AuthxUser user, IEnumerable<string> roles);
}