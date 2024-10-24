using ProductImporter.Logic.Shared;
using ProductImporter.Logic.Source;
using ProductImporter.Logic.Target;
using ProductImporter.Logic.Transformation;

namespace ProductImporter.Logic;

public class ProductImporter
{
    private readonly IProductSource _productSource;
    private readonly IProductTarget _productTarget;
    private readonly IImportStatistics _importStatistics;
    private readonly IProductTransformer _productTransformer;

    public ProductImporter(
        IProductSource productSource,
        IProductTarget productTarget,
        IImportStatistics importStatistics,
        IProductTransformer productTransformer)
    {
        _productSource = productSource;
        _productTarget = productTarget;
        _importStatistics = importStatistics;
        _productTransformer = productTransformer;
    }

    public void Run()
    {
        _productSource.Open();
        _productTarget.Open();

        while (_productSource.HasMoreProducts())
        {
            var product = _productSource.GetNextProduct();

            var transformedProduct = _productTransformer.ApplyTransformations(product);

            _productTarget.AddProduct(transformedProduct);
        }

        _productSource.Close();
        _productTarget.Close();

        Console.WriteLine("Importing complete!");
        Console.WriteLine(_importStatistics.GetStatistics());
    }
}