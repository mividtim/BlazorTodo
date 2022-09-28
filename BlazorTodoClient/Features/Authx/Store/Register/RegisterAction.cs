using BlazorTodoDtos.Authx;

namespace BlazorTodoClient.Features.Authx.Store.Register;

public class RegisterAction
{
    public RegisterAction(CreateUserDto dto) =>
        CreateUserDto = dto;

    public CreateUserDto CreateUserDto { get; }
}