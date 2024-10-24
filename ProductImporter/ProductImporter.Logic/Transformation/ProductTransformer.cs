using Microsoft.Extensions.DependencyInjection;
using ProductImporter.Logic.Shared;
using ProductImporter.Logic.Transformations;
using ProductImporter.Model;

namespace ProductImporter.Logic.Transformation;

public class ProductTransformer : IProductTransformer
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IImportStatistics _importStatistics;

    public ProductTransformer(IServiceScopeFactory serviceScopeFactory, IImportStatistics importStatistics)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _importStatistics = importStatistics;
    }

    public Product ApplyTransformations(Product product)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var transformationContext = scope.ServiceProvider.GetRequiredService<IProductTransformationContext>();
        transformationContext.SetProduct(product);

        var transformations = scope.ServiceProvider.GetRequiredService<IEnumerable<IProductTransformation>>();

        Thread.Sleep(2000);

        foreach (var transformation in transformations)
        {
            transformation.Execute();
        }

        if (transformationContext.IsProductChanged())
        {
            _importStatistics.IncrementTransformationCount();
        }

        return transformationContext.GetProduct();
    }
}
