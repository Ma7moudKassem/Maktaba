namespace Maktaba.Services.Order.Domain;

public class User
{
    [MaxLength(20)]
    public string FirstName { get; set; } = null!;

    [MaxLength(20)]
    public string LastName { get; set; } = null!;

    [MaxLength(20)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Key]
    [MaxLength(20)]
    public string UserName { get; set; } = null!;

    [MaxLength(100)]
    public string FullAddress { get; set; } = null!;
}