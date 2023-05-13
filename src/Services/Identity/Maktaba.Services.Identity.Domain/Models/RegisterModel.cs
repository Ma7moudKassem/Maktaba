namespace Maktaba.Services.Identity.Domain;

public class RegisterModel
{
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(128)]
    public string Email { get; set; } = null!;

    [StringLength(256)]
    public string Password { get; set; } = null!;

    [StringLength(500)]
    public string FullAddress { get; set; } = null!;
}
