using ProductImporter.Model;
using Microsoft.EntityFrameworkCore;

namespace ProductImporter.Target.EntityFramework;

public class TargetContext : DbContext
{
    public TargetContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Price);
    }
}
