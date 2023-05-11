namespace Maktaba.Services.Order.Domain;

public class Book : BaseEntity
{
    [MaxLength(50)]
    public string Title { get; set; } = null!;
    public double Price { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
}