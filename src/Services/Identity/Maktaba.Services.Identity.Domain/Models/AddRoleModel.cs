namespace Maktaba.Services.Identity.Domain;

public class AddRoleModel
{
    public string UserId { get; set; } = null!;
    public string Role { get; set; } = null!;
}
