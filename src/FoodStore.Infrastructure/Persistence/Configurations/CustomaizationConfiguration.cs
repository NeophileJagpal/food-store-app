using FoodStore.Domain.Entities.Aggregates.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.Infrastructure.Persistence.Configurations
{
    public class CustomaizationConfiguration : IEntityTypeConfiguration<Customization>
    {
        public void Configure(EntityTypeBuilder<Customization> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.Cost)
            .IsRequired();

            builder.OwnsOne(o => o.Cost);
        }
    }
}