using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Services.Interfaces;
using Product.Types.Constants;
using Product.Types.DTOs;
using Product.Types.Enums;
using Product.Types.Models;
using System;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductRecord>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation("Unable to retrieve the product by id, invalid id");

                    return BadRequest("Unable to retrieve the product by id, invalid id");
                }

                _logger.LogInformation("Retrieving product by id");

                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogInformation($"Unable to retrieve the product by id, the product with id: {id} does not exist");

                    return NotFound($"Unable to retrieve the product by id, the product with id: {id} does not exist");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("Unable to retrieve the product by id, an error occurred");
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet("{name}")]
        public async Task<ActionResult<PagedResults<ProductRecord>>> ListByName(
            string name, 
            int? page = Integers.MIN_PAGE_NUMBER, 
            int? pageSize = Integers.MIN_PAGE_SIZE)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    _logger.LogInformation("Unable to retrieve products by name, invalid name");

                    return BadRequest("Unable to retrieve products by name, invalid name");
                }

                _logger.LogInformation("Retrieving products by name");

                var products = await _productService.ListByNameAsync(name, page.Value, pageSize.Value);
                if (products?.TotalResults == 0)
                {
                    _logger.LogInformation($"Unable to retrieve products by named, there are no products with the name: {name}");

                    return NotFound($"Unable to retrieve products by named, there are no products with the name: {name}");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("Unable to retrieve product by name, an error occurred");
            }
        }

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<ActionResult<ProductRecord>> Create([FromBody] CreateProductDto product)
        {
            try
            {
                _logger.LogInformation("Creating product");

                var mappedProduct = new ProductRecord
                {
                    Id = product.Id,
                    Name = product.Name
                };

                var (createdProduct, result) = await _productService.CreateProduct(mappedProduct);
                switch (result)
                {
                    case ProductCommandResult.Duplicate:
                        return BadRequest($"Unable to create the product, product with id: {createdProduct.Id} already exists");
                    case ProductCommandResult.Error:
                        return BadRequest("Unable to create the product, an error occurred while creating the product");
                    default:
                        return Created("/product", createdProduct);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("Unable to create the product, an error occurred");
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto product)
        {
            try
            {
                _logger.LogInformation("Updating product");

                var mappedProduct = new ProductRecord
                {
                    Id = product.Id,
                    Name = product.Name
                };

                var result = await _productService.UpdateProduct(mappedProduct);
                switch (result)
                {
                    case ProductCommandResult.Created:
                        return Created("/product", mappedProduct);
                    case ProductCommandResult.Error:
                        return BadRequest("Unable to update the product, an error occurred while updating the product");
                    default:
                        return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("Unable to update the product, an error occurred");
            }
        }
    }
}
