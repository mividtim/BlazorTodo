namespace BlazorTodoDtos.Authx;

public class UserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? GivenName { get; set; }
    public string? Surname { get; set; }
    public ENameOrder NameOrder { get; set; }
    public string? PreferredName { get; set; }

    public string? FullName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(GivenName) && string.IsNullOrWhiteSpace(Surname)) return null;
            if (string.IsNullOrWhiteSpace(GivenName)) return Surname;
            if (string.IsNullOrWhiteSpace(Surname)) return GivenName;
            return NameOrder switch
            {
                ENameOrder.GivenNameFirst => $"{GivenName} {Surname}",
                ENameOrder.SurnameFirst => $"{Surname} {GivenName}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}