namespace Maktaba.Services.Identity.Domain;

public class TokenRequestModel
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
