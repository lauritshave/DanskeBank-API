using DanskeBank.Models;

namespace DanskeBank.Repositories
{
    public class InMemoryProductsRepository : IProductsRepository
    {
        private readonly List<Product> products = new()
        {
            new Product { Id = Guid.NewGuid(), Name = "Danske Bank Marathon T‑shirt", Price = 175, CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Danske Bank Halvmarathon T‑shirt", Price = 160, CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Danske Bank DHL T‑shirt", Price = 150, CreatedDate = DateTimeOffset.UtcNow },
            new Product { Id = Guid.NewGuid(), Name = "Danske Bank 10km T‑shirt", Price = 140, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IReadOnlyList<Product> GetProducts() => products;

        public Product? GetProduct(Guid id) =>
            products.FirstOrDefault(p => p.Id == id);

        public void PublishProduct(Product product)
        {
            products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var index = products.FindIndex(existing => existing.Id == product.Id);
            products[index] = product;
        }

        public void DeleteProduct(Guid id)
        {
            var index = products.FindIndex(existing => existing.Id == id);
            products.RemoveAt(index);
        }
    }
}
