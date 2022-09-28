using BlazorTodoDtos.Authx;

namespace BlazorTodoClient.Features.Authx.Store;

public interface IAuthxService
{
    public const string LocalStorageKeyIdentityToken = "identityToken";

    void Register(CreateUserDto dto);
    void Login(CreateAuthxTokensDto dto);
    void Logout();
}