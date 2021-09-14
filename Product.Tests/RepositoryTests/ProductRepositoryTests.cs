using Microsoft.EntityFrameworkCore;
using Product.Repositories.Implementations;
using Product.Types.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product.Tests.RepositoryTests
{
    public class ProductRepositoryTests : ProductTestBase
    {
        public ProductRepositoryTests() : 
            base(
                new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase("TestProductDb").Options
            )
        {
        }

        [Fact]
        public void Given_zero_id_when_product_exists_then_get_by_id_should_not_return_product()
        {
            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.GetByIdAsync(0).Result;

            Assert.Null(actual);
        }

        [Fact]
        public void Given_negative_id_when_product_exists_then_get_by_id_should_not_return_product()
        {
            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.GetByIdAsync(-1).Result;

            Assert.Null(actual);
        }

        [Fact]
        public void Given_valid_id_when_product_exists_then_get_by_id_should_return_product()
        {
            var expected = new ProductRecord
            {
                Id = 1,
                Name = "Apple"
            };

            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.GetByIdAsync(1).Result;

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void Given_valid_id_when_product_not_exists_then_get_by_id_should_return_product()
        {
            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.GetByIdAsync(5).Result;

            Assert.Null(actual);
        }

        [Fact]
        public void Given_empty_name_when_product_exists_then_list_by_name_should_not_return_product()
        {
            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.ListByNameAsync(string.Empty, 1, 10).Result;

            Assert.Empty(actual.Results);
        }

        [Fact]
        public void Given_null_name_when_product_exists_then_list_by_name_should_not_return_product()
        {
            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.ListByNameAsync(null, 1, 10).Result;

            Assert.Empty(actual.Results);
        }

        [Fact]
        public void Given_valid_name_when_one_product_exists_then_list_by_name_should_return_one_product()
        {
            var expected = new List<ProductRecord>
            {
                new ProductRecord
                {
                    Id = 1,
                    Name = "Apple"
                }
            };

            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.ListByNameAsync("Apple", 1, 10).Result;

            Assert.NotEmpty(actual.Results);
            Assert.Equal(expected.Count, actual.Results.Count());
            Assert.Equal(expected.First().Id, actual.Results.First().Id);
            Assert.Equal(expected.First().Name, actual.Results.First().Name);
        }

        [Fact]
        public void Given_valid_name_when_multiple_product_exists_then_list_by_name_should_return_multiple_products()
        {
            var expected = new List<ProductRecord>
            {
                new ProductRecord
                {
                    Id = 3,
                    Name = "Orange"
                },
                new ProductRecord
                {
                    Id = 4,
                    Name = "Orange"
                }
            };

            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.ListByNameAsync("Orange", 1, 10).Result;

            Assert.NotEmpty(actual.Results);
            Assert.Equal(expected.Count, actual.Results.Count());
        }

        [Fact]
        public void Given_valid_name_when_product_not_exist_then_list_by_name_should_not_return_products()
        {
            using var context = new ProductContext(Options);

            var repository = new ProductRepository(context);

            var actual = repository.ListByNameAsync("Watermelon", 1, 10).Result;

            Assert.Empty(actual.Results);
        }
    }
}
