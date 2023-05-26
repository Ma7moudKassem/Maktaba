namespace Maktaba.Services.Orders.Infrastructure.EntityConfigurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BooktId)
               .IsRequired();

        builder.Property(x => x.BookTitle)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.BookPrice)
               .IsRequired();

        builder.Property(x => x.Quantity)
               .IsRequired();
    }
}
