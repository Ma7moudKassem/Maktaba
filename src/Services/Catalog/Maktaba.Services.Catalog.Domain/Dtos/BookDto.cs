namespace Maktaba.Services.Catalog.Domain;

public class BookDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = null!;
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
}