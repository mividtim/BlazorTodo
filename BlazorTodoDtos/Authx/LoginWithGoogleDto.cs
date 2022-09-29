using System.ComponentModel.DataAnnotations;

namespace BlazorTodoDtos.Authx;

public class LoginWithGoogleDto
{
    [Required(ErrorMessage = "Credential is required")]
    public string Credential { get; set; }
}