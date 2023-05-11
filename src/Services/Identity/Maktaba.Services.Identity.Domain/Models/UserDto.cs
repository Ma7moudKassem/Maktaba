namespace Maktaba.Services.Identity.Domain;

public class UserDto
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? UserName { get; set; }
    public string FullAddress { get; set; } = null!;
}