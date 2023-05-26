namespace Maktaba.Services.Orders.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasMany(x => x.PaymentMethods)
               .WithOne()
               .HasForeignKey("UserId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
