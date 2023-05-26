namespace Maktaba.Services.Orders.Infrastructure.EntityConfigurations;

public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    const int MAX_LENGTH = 200;
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CardHolderName)
               .IsRequired()
               .HasMaxLength(MAX_LENGTH);

        builder.Property(x => x.CardNumber)
               .IsRequired()
               .HasMaxLength(MAX_LENGTH);

        builder.Property(x => x.SecurityNumber)
               .IsRequired()
               .HasMaxLength(MAX_LENGTH);

        builder.Property(x => x.ExpirationDate)
               .IsRequired();

        builder.Property(x => x.CardType)
               .IsRequired();
    }
}
