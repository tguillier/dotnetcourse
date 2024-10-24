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
            services.AddScoped<IProductTransformation, NameDecapitaliser>();

            if (options.EnableCurrencyNormalizer)
            {
                services.AddScoped<IProductTransformation, CurrencyNormalizer>();
            }

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IProductTransformation, ReferenceAdder>();
            services.AddScoped<IReferenceGenerator, ReferenceGenerator>();

            services.AddSingleton<IInternalCounter, InternalCounter>();

            return services;
        }
    }
}
