using BlazorTodoDtos.Authx;
using Microsoft.AspNetCore.Identity;

namespace BlazorTodoService.Features.Authx;

public class AuthxUser : IdentityUser<Guid>
{
    public string? GivenName { get; set; }
    public string? Surname { get; set; }
    public ENameOrder NameOrder { get; set; } = ENameOrder.GivenNameFirst;
    public string? PreferredName { get; set; }

    public UserDto ToDto() => new()
    {
        UserName = UserName,
        Email = Email,
        PhoneNumber = PhoneNumber,
        GivenName = GivenName,
        Surname = Surname,
        NameOrder = NameOrder,
        PreferredName = PreferredName
    };
}