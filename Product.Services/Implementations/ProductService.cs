using Microsoft.Extensions.Logging;
using Product.Repositories.Interfaces;
using Product.Services.Interfaces;
using Product.Types.Enums;
using Product.Types.Models;
using System;
using System.Threading.Tasks;

namespace Product.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository repository,
            ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<(ProductRecord, ProductCommandResult)> CreateProduct(ProductRecord product)
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(product.Id);
                if (existingProduct != null)
                {
                    return (existingProduct, ProductCommandResult.Duplicate);
                }

                return (await _repository.CreateProduct(product), ProductCommandResult.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return (null, ProductCommandResult.Error);
            }
        }

        public async Task<ProductRecord> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        public async Task<PagedResults<ProductRecord>> ListByNameAsync(string name, int page, int pageSize)
        {
            try
            {
                return await _repository.ListByNameAsync(name, page, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        public async Task<ProductCommandResult> UpdateProduct(ProductRecord product)
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(product.Id);
                if (existingProduct == null)
                {
                    await _repository.CreateProduct(product);

                    return ProductCommandResult.Created;
                }

                await _repository.UpdateProduct(product);

                return ProductCommandResult.Updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return ProductCommandResult.Error;
            }
        }
    }
}
