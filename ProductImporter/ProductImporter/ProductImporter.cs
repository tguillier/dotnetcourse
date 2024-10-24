using ProductImporter.Source;
using ProductImporter.Target;

namespace ProductImporter;

public class ProductImporter
{
    private readonly ProductSource _productSource = new();
    private readonly ProductTarget _productTarget = new();

    public void Run()
    {
        _productSource.Open();
        _productTarget.Open();

        while (_productSource.HasMoreProducts())
        {
            var product = _productSource.GetNextProduct();
            _productTarget.AddProduct(product);
        }

        _productSource.Close();
        _productTarget.Close();
    }
}