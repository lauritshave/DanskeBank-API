using System.ComponentModel.DataAnnotations;

namespace DanskeBank.Dtos
{
    /// <summary>
    /// DTO for publishing a new product with required name and price.
    /// </summary>
    public record PublishProductDto
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required]
        public string Name { get; init; }

        /// <summary>
        /// Price of the product (must be between 1 and 500).
        /// </summary>
        [Required]
        [Range(1, 500)]
        public decimal Price { get; init; }
    }
}
