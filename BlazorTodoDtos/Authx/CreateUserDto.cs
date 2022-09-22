using System.ComponentModel.DataAnnotations;

namespace BlazorTodoDtos.Authx;

public class CreateUserDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    public string? GivenName { get; set; }

    public string? Surname { get; set; }

    public ENameOrder? NameOrder { get; set; }

    public string? PreferredName { get; set; }

    public static CreateUserDto Normalize(CreateUserDto input) => new() {
        Email = input.Email.Trim(),
        Password = input.Password,
        PhoneNumber = TrimAndNullNormalize(input.PhoneNumber),
        GivenName = TrimAndNullNormalize(input.GivenName),
        Surname = TrimAndNullNormalize(input.Surname),
        NameOrder = input.NameOrder ?? ENameOrder.GivenNameFirst,
        PreferredName = TrimAndNullNormalize(input.PreferredName)
    };

    private static string? TrimAndNullNormalize(string? input) =>
        string.IsNullOrWhiteSpace(input?.Trim()) ? null : input.Trim();
}