using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Repositories.Interfaces;
using Product.Types.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _repository;

        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
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

                var product = await _repository.GetAsync(id);
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
        public async Task<ActionResult<IEnumerable<ProductRecord>>> ListByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    _logger.LogInformation("Unable to retrieve products by name, invalid name");

                    return BadRequest("Unable to retrieve products by name, invalid name");
                }

                var products = await _repository.ListByNameAsync(name);
                if (!products.Any())
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
