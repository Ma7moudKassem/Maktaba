namespace Maktaba.Domain;

public class BookDto
{
    public string Title { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string? Description { get; set; }
    public string? CategoryName { get; set; }
}