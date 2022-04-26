namespace Coyote.Catalog.PostgreSQL;

using Coyote.Catalog.PostgreSQL.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

internal class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<ProductState> Products => Set<ProductState>();
}
