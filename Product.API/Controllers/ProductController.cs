using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Types;
using System;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRecord>> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Unable to retrieve the product by id, invalid id");
                }

                return Ok(await Task.Run(() => new ProductRecord { Id = id, Name = $"{id}" }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("Unable to retrieve the product by id, an error occurred");
            }
        }
    }
}
