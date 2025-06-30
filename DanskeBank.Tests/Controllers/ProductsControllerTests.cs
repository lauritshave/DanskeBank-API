using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DanskeBank.Controllers;
using DanskeBank.Models;
using DanskeBank.Repositories;
using DanskeBank.Dtos;

namespace DanskeBank.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsRepository> mockRepo = new();

        private Product CreateProduct(Guid? id = null) => new()
        {
            Id = id ?? Guid.NewGuid(),
            Name = "Test Product",
            Price = 100,
            CreatedDate = DateTimeOffset.UtcNow
        };

        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            var products = new[] { CreateProduct(), CreateProduct() };
            mockRepo.Setup(r => r.GetProducts()).Returns(products);

            var controller = new ProductsController(mockRepo.Object);
            var result = controller.GetProducts();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetProduct_UnknownId_ReturnsNotFound()
        {
            mockRepo.Setup(r => r.GetProduct(It.IsAny<Guid>())).Returns((Product?)null);

            var controller = new ProductsController(mockRepo.Object);
            var result = controller.GetProduct(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetProduct_KnownId_ReturnsProductDto()
        {
            var product = CreateProduct();
            mockRepo.Setup(r => r.GetProduct(product.Id)).Returns(product);

            var controller = new ProductsController(mockRepo.Object);
            var result = controller.GetProduct(product.Id);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<ProductDto>(ok.Value);
            Assert.Equal(product.Id, dto.Id);
        }

        [Fact]
        public void PublishProduct_ValidDto_ReturnsCreated()
        {
            var controller = new ProductsController(mockRepo.Object);
            var dto = new PublishProductDto { Name = "New", Price = 99 };

            var result = controller.PublishProduct(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var product = Assert.IsType<ProductDto>(created.Value);
            Assert.Equal("New", product.Name);
        }

        [Fact]
        public void UpdateProduct_UnknownId_ReturnsNotFound()
        {
            mockRepo.Setup(r => r.GetProduct(It.IsAny<Guid>())).Returns((Product?)null);

            var controller = new ProductsController(mockRepo.Object);
            var result = controller.UpdateProduct(Guid.NewGuid(), new UpdateProductDto());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteProduct_UnknownId_ReturnsNotFound()
        {
            mockRepo.Setup(r => r.GetProduct(It.IsAny<Guid>())).Returns((Product?)null);

            var controller = new ProductsController(mockRepo.Object);
            var result = controller.DeleteProduct(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteProduct_ExistingId_ReturnsNoContent()
        {
            var product = CreateProduct();
            mockRepo.Setup(r => r.GetProduct(product.Id)).Returns(product);

            var controller = new ProductsController(mockRepo.Object);
            var result = controller.DeleteProduct(product.Id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
