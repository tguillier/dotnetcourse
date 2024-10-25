using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.CompositionRoot;
using ProductImporter.Logic.Source;

using var host = Host.CreateDefaultBuilder()
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices((context, services) =>
    {
        services.AddProductImporter(context);
    })
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterProductImporter();
    })
    .Build();

var productImporter = host.Services.GetRequiredService<ProductImporter.Logic.ProductImporter>();
await productImporter.RunAsync();

Console.ReadKey();