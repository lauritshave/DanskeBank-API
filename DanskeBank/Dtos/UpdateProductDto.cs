using System.ComponentModel.DataAnnotations;

namespace DanskeBank.Dtos
{
    /// <summary>
    /// DTO for updating an existing product's name and price.
    /// </summary>
    public record UpdateProductDto
    {
        /// <summary>
        /// Updated name of the product.
        /// </summary>
        [Required]
        public string Name { get; init; }

        /// <summary>
        /// Updated price of the product (must be between 1 and 500).
        /// </summary>
        [Required]
        [Range(1, 500)]
        public decimal Price { get; init; }
    }
}