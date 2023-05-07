namespace Maktaba.Domain;

public class LibraryBook : BaseEntity
{
    public Guid LibraryId { get; set; }

    public Guid BookId { get; set; }
    public Book? Book { get; set; }
}