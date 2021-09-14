using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Repositories.Context;
using Product.Types.Models;
using System;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductContext _context;

        public ProductController(
            ILogger<ProductController> logger,
            ProductContext context)
        {
            _logger = logger;
            _context = context;
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

                var product = await _context.Products.FindAsync(id);
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
    }
}
