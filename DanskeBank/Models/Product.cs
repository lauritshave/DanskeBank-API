using System.ComponentModel.DataAnnotations;

namespace DanskeBank.Models
{
    public record Product
    {
        public Guid Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}
