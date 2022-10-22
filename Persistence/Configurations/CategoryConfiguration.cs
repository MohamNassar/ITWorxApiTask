using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey(Category => Category.Id);

            builder.Property(Product => Product.Id).ValueGeneratedOnAdd();

            builder.Property(Category => Category.Name).HasMaxLength(60).IsRequired();

            builder.HasMany(Category => Category.Products)
                .WithOne()
                .HasForeignKey(Product => Product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
