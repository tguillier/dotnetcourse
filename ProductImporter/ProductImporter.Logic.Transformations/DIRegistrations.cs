using Microsoft.Extensions.DependencyInjection;
using ProductImporter.Logic.Transformations.Util;

namespace ProductImporter.Logic.Transformations
{
    public static class DIRegistrations
    {
        public static IServiceCollection AddProductTransformations(
            this IServiceCollection services,
            Action<ProductTransformationOptions> optionsProvider)
        {
            var options = new ProductTransformationOptions();
            optionsProvider(options);

            services.AddScoped<IProductTransformationContext, ProductTransformationContext>();
            services.AddScoped<INameDecapitaliser, NameDecapitaliser>();

            if (options.EnableCurrencyNormalizer)
            {
                services.AddScoped<ICurrencyNormalizer, CurrencyNormalizer>();
            }
            else
            {
                services.AddScoped<ICurrencyNormalizer, NullCurrencyNormalizer>();
            }

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IReferenceAdder, ReferenceAdder>();
            services.AddScoped<IReferenceGenerator, ReferenceGenerator>();

            services.AddSingleton<IInternalCounter, InternalCounter>();

            return services;
        }
    }
}
