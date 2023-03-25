using Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CategoryBrandEntityTypeConfiguration : IEntityTypeConfiguration<CategoryBrand>
{
    public void Configure(EntityTypeBuilder<CategoryBrand> builder)
    {
        builder.Property(cb => cb.Brand)
            .IsRequired()
            .HasMaxLength(100);
    }
}