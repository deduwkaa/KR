using OOPShop.Models;

namespace OOPShop.Services.Interfaces
{
    public interface IProductService
    {
        void Add(Product product);
        bool Delete(int id);
        List<Product> GetAll();
        Product? GetById(int id);
        void Save();
    }
}
