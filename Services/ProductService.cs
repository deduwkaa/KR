using OOPShop.Models;
using OOPShop.Repositories.Interfaces;
using OOPShop.Services.Interfaces;

namespace OOPShop.Services
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Add(Product product)
        {
            productRepository.Add(product);
        }

        public bool Delete(int id)
        {
            return productRepository.Delete(id);
        }

        public List<Product> GetAll()
        {
           return productRepository.GetAll();
        }

        public Product? GetById(int id)
        {
            return productRepository.GetById(id);
        }

        public void Save()
        {
            productRepository.Save();
        }
    }
}
