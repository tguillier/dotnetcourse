using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.Logic.Shared;
using ProductImporter.Logic.Source;
using ProductImporter.Logic.Target;
using ProductImporter.Logic.Target.EntityFramework;
using ProductImporter.Logic.Transformation;
using System;

namespace ProductImporter.Logic
{
    public static class DIRegistrations
    {
        public static IServiceCollection AddProductImporterLogic(this IServiceCollection services, HostBuilderContext context)
        {
            services.AddTransient<IPriceParser, PriceParser>();
            services.AddHttpClient<IProductSource, HttpProductSource>()
                .ConfigureHttpClient(client =>
                {
                    var baseAdress = context.Configuration.GetValue<string>($"{HttpProductSourceOptions.SectionName}:{nameof(client.BaseAddress)}");
                    client.BaseAddress = new Uri(baseAdress);
                });

            services.AddTransient<CsvProductTarget>();
            services.AddTransient<OldCsvProductTarget>();
            services.AddTransient<IProductTarget>(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                if (configuration.GetValue<bool>("EnableCsvWriter"))
                {
                    return serviceProvider.GetRequiredService<CsvProductTarget>();
                }

                return serviceProvider.GetRequiredService<OldCsvProductTarget>();
            });
            services.AddTransient<IProductFormatter, ProductFormatter>();

            services.AddTransient<ProductImporter>();

            services.AddSingleton<ImportStatistics>();
            services.AddSingleton<IWriteImportStatistics>(serviceProvider => serviceProvider.GetRequiredService<ImportStatistics>());
            services.AddSingleton<IGetImportStatistics>(serviceProvider => serviceProvider.GetRequiredService<ImportStatistics>());

            services.AddTransient(serviceProvider =>
            {
                return new Lazy<IProductTransformer>(() =>
                {
                    var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                    var importStatistics = serviceProvider.GetRequiredService<IWriteImportStatistics>();

                    return new ProductTransformer(serviceScopeFactory, importStatistics);
                });
            });

            services.AddScoped(_ => new TargetContext(context.Configuration.GetConnectionString(nameof(TargetContext))));

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

            services.AddOptions<HttpProductSourceOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration.GetSection(HttpProductSourceOptions.SectionName).Bind(options);
                });

            return services;
        }
    }
}
