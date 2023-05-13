namespace Maktaba.Services.Order.Infrastructure;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Order> builder)
    {
        builder.ToTable(nameof(Domain.Order));

        builder.Property(x => x.UserName)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(k => k.UserName);

        builder.HasMany(x => x.OrderBooks)
               .WithOne()
               .HasForeignKey(k => k.OrderId);
    }
}
