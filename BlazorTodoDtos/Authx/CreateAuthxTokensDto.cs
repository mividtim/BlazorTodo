using System.ComponentModel.DataAnnotations;

namespace BlazorTodoDtos.Authx;

public class CreateAuthxTokensDto
{
    [Required(ErrorMessage = "User name is required")]
    [EmailAddress]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}