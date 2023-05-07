namespace Maktaba.Infrastructure;

public class LibraryBookConfiguration : IEntityTypeConfiguration<LibraryBook>
{
    public void Configure(EntityTypeBuilder<LibraryBook> builder)
    {
        builder.ToTable(nameof(LibraryBook));

        builder.Property(e => e.LibraryId)
            .IsRequired();

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.HasOne(e => e.Book)
            .WithMany()
            .HasForeignKey(e => e.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
