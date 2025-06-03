using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestructure.EntitiesConfiguration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            var productCategoryNameConverter = new ValueConverter<Name, string>(
                productCategoryName => productCategoryName.Value,
                value => new Name(value)
            );

            builder.HasKey(x => x.ProductCategoryId);
            builder.Property(x => x.ProductCategoryId).ValueGeneratedOnAdd();


            builder.Property(u => u.ProductCategoryName)
                .HasConversion(productCategoryNameConverter)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

            builder.HasMany(x => x.Products)
                .WithOne(x => x.ProductCategory)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ProductCategoryName).IsUnique();
        }
    }
}
