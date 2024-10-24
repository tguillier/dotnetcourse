using Microsoft.Extensions.Options;
using ProductImporter.Logic.Shared;
using ProductImporter.Model;
using System.Text.Json;

namespace ProductImporter.Logic.Source;

public class HttpProductSource : IProductSource
{
    private readonly IOptions<HttpProductSourceOptions> _productSourceOptions;
    private readonly HttpClient _httpClient;
    private readonly IImportStatistics _importStatistics;

    private readonly Queue<Product> _remainingProducts;

    public HttpProductSource(
        IOptions<HttpProductSourceOptions> productSourceOptions,
        HttpClient httpClient,
        IImportStatistics importStatistics)
    {
        _productSourceOptions = productSourceOptions;
        _httpClient = httpClient;
        _importStatistics = importStatistics;

        _remainingProducts = new Queue<Product>();
    }

    public async Task OpenAsync()
    {
        using var productStream = await _httpClient.GetStreamAsync(_productSourceOptions.Value.ProductsUri);
        var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(productStream);

        foreach (var product in products)
        {
            _remainingProducts.Enqueue(product);
        }
    }

    public bool HasMoreProducts()
    {
        return _remainingProducts.Count > 0;
    }

    public Product GetNextProduct()
    {
        var product = _remainingProducts.Dequeue();
        _importStatistics.IncrementImportCount();

        return product;
    }

    public void Close()
    {
        // Do nothing
    }
}