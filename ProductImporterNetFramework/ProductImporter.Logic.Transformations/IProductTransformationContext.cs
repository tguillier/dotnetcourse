using ProductImporter.Model;

namespace ProductImporter.Logic.Transformations
{
    public interface IProductTransformationContext
    {
        void SetProduct(Product product);
        Product GetProduct();
        bool IsProductChanged();
    }
}
