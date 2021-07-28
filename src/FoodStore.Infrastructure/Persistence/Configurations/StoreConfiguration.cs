using FoodStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.Name)
            .IsRequired();

            builder.HasOne(p => p.Address);
            builder.HasOne(p => p.Contact);

            builder.Property(p => p.Products).HasField("_products");
        }
    }
}