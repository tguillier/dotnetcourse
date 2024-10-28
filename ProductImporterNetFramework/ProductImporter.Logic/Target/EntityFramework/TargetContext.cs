using ProductImporter.Model;
using System.Data.Entity;

namespace ProductImporter.Logic.Target.EntityFramework
{
    public class TargetContext : DbContext
    {
        public TargetContext(string connectionString) : base(connectionString)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Price);
        }
    }
}
