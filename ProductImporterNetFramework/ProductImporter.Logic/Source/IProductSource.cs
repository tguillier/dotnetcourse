using ProductImporter.Model;
using System.Threading.Tasks;

namespace ProductImporter.Logic.Source
{
    public interface IProductSource
    {
        void Close();
        Product GetNextProduct();
        bool HasMoreProducts();
        Task OpenAsync();
    }
}