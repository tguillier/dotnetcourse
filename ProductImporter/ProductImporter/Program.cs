﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.Shared;
using ProductImporter.Source;
using ProductImporter.Target;
using ProductImporter.Transformation;
using ProductImporter.Transformation.Transformations;
using ProductImporter.Util;

using var host = Host.CreateDefaultBuilder()
    .UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateScopes = true;
    })
    .ConfigureServices(services =>
    {
        services.AddTransient<Configuration>();

        services.AddTransient<IPriceParser, PriceParser>();
        services.AddTransient<IProductFormatter, ProductFormatter>();

        services.AddTransient<IProductSource, ProductSource>();
        services.AddTransient<IProductTarget, ProductTarget>();

        services.AddTransient<ProductImporter.ProductImporter>();

        services.AddSingleton<IImportStatistics, ImportStatistics>();

        services.AddTransient<IProductTransformer, ProductTransformer>();

        services.AddScoped<IProductTransformationContext, ProductTransformationContext>();
        services.AddScoped<INameDecapitaliser, NameDecapitaliser>();
        services.AddScoped<ICurrencyNormalizer, CurrencyNormalizer>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IReferenceAdder, ReferenceAdder>();
        services.AddScoped<IReferenceGenerator, ReferenceGenerator>();

        services.AddSingleton<IInternalCounter, InternalCounter>();

    })
    .Build();

var productImporter = host.Services.GetRequiredService<ProductImporter.ProductImporter>();
productImporter.Run();

Console.ReadKey();