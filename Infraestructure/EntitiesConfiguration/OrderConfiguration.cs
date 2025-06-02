using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.EntitiesConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                   .ValueGeneratedOnAdd();

            builder.Property(o => o.OrderDate)
                   .IsRequired();

            builder.HasOne(o => o.Client)
                   .WithMany()
                   .HasForeignKey(o => o.ClientId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.OrderStatus)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
