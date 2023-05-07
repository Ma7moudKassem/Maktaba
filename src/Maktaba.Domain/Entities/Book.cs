namespace Maktaba.Domain;

public class Book : BaseEntity
{
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public string Title { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string? Description { get; set; }
}