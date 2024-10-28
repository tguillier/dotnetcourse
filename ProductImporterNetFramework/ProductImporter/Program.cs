using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductImporter.CompositionRoot;
using System;
using System.Threading.Tasks;

namespace ProductImporter
{
    public static class Program
    {
        public static async Task Main()
        {
            using (var host = CreateHost())
            {
                var productImporter = host.Services.GetRequiredService<Logic.ProductImporter>();
                await productImporter.RunAsync();
            }

            Console.ReadKey();
        }

        private static IHost CreateHost()
        {
            return Host.CreateDefaultBuilder()
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
        }
    }
}
