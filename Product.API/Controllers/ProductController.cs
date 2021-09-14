using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Services.Interfaces;
using Product.Types.Constants;
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

                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogInformation($"Unable to retrieve the product by id, the product with id: {id} does not exist");

                    return BadRequest($"Unable to retrieve the product by id, the product with id: {id} does not exist");
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

                var products = await _productService.ListByNameAsync(name, page.Value, pageSize.Value);
                if (products?.TotalResults == 0)
                {
                    _logger.LogInformation($"Unable to retrieve products by named, there are no products with the name: {name}");

                    return BadRequest($"Unable to retrieve products by named, there are no products with the name: {name}");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("Unable to retrieve product by name, an error occurred");
            }
        }
    }
}
