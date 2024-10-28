using Microsoft.Extensions.DependencyInjection;
using ProductImporter.Logic.Shared;
using ProductImporter.Logic.Target.EntityFramework;
using ProductImporter.Model;
using System.Data.Entity.Migrations;

namespace ProductImporter.Logic.Target
{
    public class SqlProductTarget : IProductTarget
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IWriteImportStatistics _importStatistics;

        public SqlProductTarget(
            IServiceScopeFactory serviceScopeFactory,
            IWriteImportStatistics importStatistics)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _importStatistics = importStatistics;
        }

        public void AddProduct(Product product)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TargetContext>();

                _importStatistics.IncrementOutputCount();

                context.Products.AddOrUpdate(product);

                context.SaveChanges();
            }
        }

        public void Close()
        {
            // Nothing to do.
        }

        public void Open()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TargetContext>();

                context.Database.CreateIfNotExists();
            }
        }
    }
}
