using Autofac;
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

            return services;
        }

        public static ContainerBuilder RegisterProductImporter(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ProductTransformationsModule(new ProductTransformationOptions
            {
                EnableCurrencyNormalizer = true
            }));

            return containerBuilder;
        }
    }
}
