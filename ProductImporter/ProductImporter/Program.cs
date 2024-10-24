﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.Shared;
using ProductImporter.Source;
using ProductImporter.Target;

using var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<Configuration>();

        services.AddTransient<IPriceParser, PriceParser>();
        services.AddTransient<IProductFormatter, ProductFormatter>();

        services.AddTransient<IProductSource, ProductSource>();
        services.AddTransient<IProductTarget, ProductTarget>();

        services.AddTransient<ProductImporter.ProductImporter>();

        services.AddSingleton<IImportStatistics, ImportStatistics>();
    })
    .Build();

var productImporter = host.Services.GetRequiredService<ProductImporter.ProductImporter>();
productImporter.Run();

Console.ReadKey();