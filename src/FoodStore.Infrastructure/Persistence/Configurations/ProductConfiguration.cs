using FoodStore.Domain.Entities.Aggregates.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.ProductInfo)
            .IsRequired();

            builder.OwnsOne(o => o.ProductInfo);

            builder.Property(p => p.Customizations).HasField("_customizations");
        }
    }
}