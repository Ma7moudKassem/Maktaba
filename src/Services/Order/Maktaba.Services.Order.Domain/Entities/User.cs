namespace Maktaba.Services.Order.Domain;

public class User
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Key]
    [MaxLength(20)]
    public string UserName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string FullAddress { get; set; } = null!;
}