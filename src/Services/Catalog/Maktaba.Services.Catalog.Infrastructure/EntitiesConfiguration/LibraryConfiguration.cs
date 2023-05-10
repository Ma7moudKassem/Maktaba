namespace Maktaba.Services.Catalog.Infrastructure;

public class LibraryConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable(nameof(Library));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(x => x.LibraryBooks)
            .WithOne()
            .HasForeignKey(e => e.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}