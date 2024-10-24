using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.Logic;
using ProductImporter.Logic.Transformations;

namespace ProductImporter.CompositionRoot
{
    public static class CompositionRoot
    {
        public static IServiceCollection AddProductImporter(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddProductImporterLogic(context);
            services.AddProductTransformations(options =>
            {
                options.EnableCurrencyNormalizer = true;
            });

            return services;
        }
    }
}
