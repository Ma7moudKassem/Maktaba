namespace Maktaba.Domain;

public class BookDto
{
    public string Title { get; set; } = null!;
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
}