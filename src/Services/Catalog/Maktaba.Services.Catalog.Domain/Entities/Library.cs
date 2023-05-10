namespace Maktaba.Services.Catalog.Domain;

public class Library : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;

    public List<LibraryBook> LibraryBooks { get; set; } = new();
}