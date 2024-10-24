using ProductImporter.Model;

namespace ProductImporter.Target
{
    public interface IProductTarget
    {
        void AddProduct(Product product);
        void Close();
        void Open();
    }
}