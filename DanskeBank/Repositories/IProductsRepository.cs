using DanskeBank.Models;

namespace DanskeBank.Repositories
{
    public interface IProductsRepository
    {
        Product? GetProduct(Guid id);
        IReadOnlyList<Product> GetProducts();
        void PublishProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);
    }
}