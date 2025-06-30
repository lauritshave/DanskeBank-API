namespace DanskeBank.Dtos
{
    /// <summary>
    /// Data Transfer Object representing a product with its key details.
    /// </summary>
    public record ProductDto
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; init; }

        /// <summary>
        /// UTC timestamp when the product was created.
        /// </summary>
        public DateTimeOffset CreatedDate { get; init; }
    }
}