namespace Maktaba.Services.Orders.Infrastructure.EntityConfigurations;

public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
{
    const int MAX_LENGTH = 200;
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Street)
               .IsRequired()
               .HasMaxLength(MAX_LENGTH);

        builder.Property(x => x.Country)
               .IsRequired()
               .HasMaxLength(MAX_LENGTH);

        builder.Property(x => x.City)
               .IsRequired()
               .HasMaxLength(MAX_LENGTH);
    }
}