namespace Coyote.Catalog.PostgreSQL.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ProductConfiguration : IEntityTypeConfiguration<ProductState>
{
    public void Configure(EntityTypeBuilder<ProductState> builder)
    {
        builder.Property(e => e.Features).HasColumnType("jsonb");
    }
}
