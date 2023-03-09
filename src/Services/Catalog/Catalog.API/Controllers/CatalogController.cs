using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Catalog.API.Repositories;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repositories;

        public CatalogController(IProductRepository repositories)
        {
            _repositories = repositories;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _repositories.GetProducts();
            return Ok(products);
        }

        //[HttpGet("{id:length(24)}", Name = "GetProduct")]
        [HttpGet]
        [Route("[action]/{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repositories.GetProduct(id);
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _repositories.GetProductByCategory(category);
            return Ok(products);
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await _repositories.GetProductByName(name);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repositories.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        // Notice: we have used "IActionResult" instead of ActionResult.
        // and "IActionResult" does not contains/returns anything like Product or IEnumerable<Product>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repositories.UpdateProduct(product));
        }

        //[HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [HttpDelete]
        [Route("[action]/{name}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductById(string name)
        {
            return Ok(await _repositories.DeleteProduct(name));
        }
    }
}
