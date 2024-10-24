using ProductImporter.Model;

namespace ProductImporter.Logic.Target
{
    public interface IProductTarget
    {
        void AddProduct(Product product);
        void Close();
        void Open();
    }
}