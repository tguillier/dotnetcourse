using ProductImporter.Model;

namespace ProductImporter.Source
{
    public interface IProductSource
    {
        void Close();
        Product GetNextProduct();
        bool HasMoreProducts();
        void Open();
    }
}