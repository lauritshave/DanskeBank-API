using DanskeBank.Dtos;
using DanskeBank.Models;
using DanskeBank.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DanskeBank.Controllers
{
    [ApiController]
    [Route("items")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository repository;

        public ProductsController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        // GET route /items?page=1&size=10 => Supply page and size parameter for pagination

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="size">Number of items per page (default is 10).</param>
        /// <response code="200">Returns the list of products.</response>
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10
        )
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 10;

            // Skip/Take => Pagination Support
            return repository
                .GetProducts()
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p => p.MapToDto());
        }

        // GET route /items/{id} => Returns one product

         /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(Guid id)
        {
            var product = repository.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product.MapToDto());
        }

        // POST route /items => Publish one product

        /// <summary>
        /// Publishes a new product.
        /// </summary>
        /// <response code="201">Product created successfully.</response>
        [HttpPost]
        public ActionResult<ProductDto> PublishProduct(PublishProductDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            repository.PublishProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product.MapToDto());
        }

        // PUT route /items/{id}

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <response code="200">Product updated successfully.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, UpdateProductDto productDto)
        {
            var existing = repository.GetProduct(id);
            if (existing is null)
            {
                return NotFound();
            }

            Product updatedProduct = existing with
            {
                Name = productDto.Name,
                Price = productDto.Price,
            };

            repository.UpdateProduct(updatedProduct);
            return Ok(updatedProduct.MapToDto());
        }

        // DELETE route /items/{id}

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <response code="204">Product deleted successfully.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var existing = repository.GetProduct(id);
            if (existing is null)
            {
                return NotFound();
            }

            repository.DeleteProduct(id);
            return NoContent();
        }
    }
}