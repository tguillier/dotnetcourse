using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.Logic.Shared;
using ProductImporter.Logic.Source;
using ProductImporter.Logic.Target;
using ProductImporter.Logic.Target.EntityFramework;
using ProductImporter.Logic.Transformation;

namespace ProductImporter.Logic
{
    public static class DIRegistrations
    {
        public static IServiceCollection AddProductImporterLogic(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddTransient<IPriceParser, PriceParser>();
            services.AddTransient<IProductFormatter, ProductFormatter>();

            services.AddTransient<IProductSource, ProductSource>();
            services.AddTransient<IProductTarget, CsvProductTarget>();

            services.AddTransient<ProductImporter>();

            services.AddSingleton<IImportStatistics, ImportStatistics>();

            services.AddTransient<IProductTransformer, ProductTransformer>();

            services.AddDbContext<TargetContext>(options =>
            {
                options.UseSqlServer(context.Configuration.GetConnectionString(nameof(TargetContext)));
            });

            services.AddOptions<CsvProductTargetOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration.GetSection(CsvProductTargetOptions.SectionName).Bind(options);
                });

            services.AddOptions<CsvProductSourceOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration.GetSection(CsvProductSourceOptions.SectionName).Bind(options);
                });

            return services;
        }
    }
}
