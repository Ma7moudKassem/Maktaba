namespace Maktaba.Services.Identity.Domain;

public class User : IdentityUser
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
    }

    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [MaxLength(500)]
    public string FullAddress { get; set; } = null!;

    public List<RefreshToken>? RefreshTokens { get; set; }
}