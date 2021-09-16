using Microsoft.Extensions.Logging;
using Product.Repositories.Interfaces;
using Product.Services.Interfaces;
using Product.Types.DTOs;
using Product.Types.Enums;
using Product.Types.Models;
using Product.Utilities.Extensions;
using System;
using System.Linq;
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

        public async Task<(ProductRecordDto, ProductCommandResult)> CreateProduct(ProductRecord product)
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(product.Id);
                if (existingProduct != null)
                {
                    return (existingProduct.Map(), ProductCommandResult.Duplicate);
                }

                var createdProduct = await _repository.CreateProduct(product);
                if (createdProduct != null)
                {
                    return (createdProduct.Map(), ProductCommandResult.Created);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return (null, ProductCommandResult.Error);
        }

        public async Task<ProductRecordDto> GetByIdAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);

                return product?.Map();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        public async Task<PagedResults<ProductRecordDto>> ListByNameAsync(string name, int page, int pageSize)
        {
            try
            {
                var products = await _repository.ListByNameAsync(name, page, pageSize);

                if (products != null)
                {
                    return new PagedResults<ProductRecordDto>
                    {
                        Page = products.Page,
                        PageSize = products.PageSize,
                        Results = products.Results.Select(product => product.Map()),
                        TotalPages = products.TotalPages
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<(ProductRecordDto, ProductCommandResult)> UpdateProduct(ProductRecord product)
        {
            try
            {
                var existingProduct = await _repository.GetByIdAsync(product.Id);
                if (existingProduct != null)
                {
                    await _repository.UpdateProduct(product);

                    return (product.Map(), ProductCommandResult.Updated);
                }

                var createdProduct = await _repository.CreateProduct(product);
                if (createdProduct != null)
                {
                    return (createdProduct.Map(), ProductCommandResult.Created);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return (null, ProductCommandResult.Error);
        }
    }
}
