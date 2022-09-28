using BlazorTodoDtos.Authx;

namespace BlazorTodoClient.Features.Authx.Store.Login;

public class LoginAction
{
    public LoginAction(CreateAuthxTokensDto dto) =>
        CreateAuthxTokensDto = dto;

    public CreateAuthxTokensDto CreateAuthxTokensDto { get; }
}