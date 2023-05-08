namespace Maktaba.Domain;

public class Book : BaseEntity
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public string Title { get; set; } = null!;
    public double Price { get; set; }
    public string? Description { get; set; }
}