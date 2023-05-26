namespace Maktaba.Services.Orders.Infrastructure.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AddressId)
               .IsRequired();

        builder.HasOne(x => x.Address)
               .WithMany()
               .HasForeignKey(x => x.AddressId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.UserId)
               .IsRequired();

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.OrderStatus)
               .IsRequired();
    }
}