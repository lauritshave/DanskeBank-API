using DanskeBank.Dtos;
using DanskeBank.Models;

namespace DanskeBank
{
    public static class Extensions
    {
        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CreatedDate = product.CreatedDate,
            };
        }
    }
}