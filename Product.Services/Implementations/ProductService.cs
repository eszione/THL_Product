using Microsoft.Extensions.Logging;
using Product.Repositories.Interfaces;
using Product.Services.Interfaces;
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

        public async Task<ProductRecord> CreateProduct(ProductRecord product)
        {
            try
            {
                return await _repository.CreateProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
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
    }
}
