using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.Logic.Shared;
using ProductImporter.Logic.Source;
using ProductImporter.Logic.Target;
using ProductImporter.Logic.Target.EntityFramework;
using ProductImporter.Logic.Transformation;
using ProductImporter.Logic.Transformations;
using ProductImporter.Logic.Transformations.Util;

namespace ProductImporter.CompositionRoot
{
    public static class CompositionRoot
    {
        public static IServiceCollection AddProductImporter(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddSingleton<Configuration>();

            services.AddTransient<IPriceParser, PriceParser>();
            services.AddTransient<IProductFormatter, ProductFormatter>();

            services.AddTransient<IProductSource, ProductSource>();
            services.AddTransient<IProductTarget, SqlProductTarget>();

            services.AddTransient<Logic.ProductImporter>();

            services.AddSingleton<IImportStatistics, ImportStatistics>();

            services.AddTransient<IProductTransformer, ProductTransformer>();

            services.AddScoped<IProductTransformationContext, ProductTransformationContext>();
            services.AddScoped<INameDecapitaliser, NameDecapitaliser>();
            services.AddScoped<ICurrencyNormalizer, CurrencyNormalizer>();

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IReferenceAdder, ReferenceAdder>();
            services.AddScoped<IReferenceGenerator, ReferenceGenerator>();

            services.AddSingleton<IInternalCounter, InternalCounter>();

            services.AddDbContext<TargetContext>(options =>
            {
                options.UseSqlServer(context.Configuration.GetConnectionString(nameof(TargetContext)));
            });

            return services;
        }
    }
}
