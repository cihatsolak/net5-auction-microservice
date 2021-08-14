using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESourcing.Products.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _productLogger;
        #endregion

        #region Ctor
        public ProductController(
            IProductRepository productRepository,
            ILogger<ProductController> productLogger)
        {
            _productRepository = productRepository;
            _productLogger = productLogger;
        }
        #endregion

        #region Methods
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            if (products is null || !products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("{id:minlength(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
            {
                _productLogger.LogError($"Product with id : {id},hasn't been found in database.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepository.InsertAsync(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            bool isSuccess = await _productRepository.UpdateAsync(product);
            if (!isSuccess)
            {
                return Problem();
            }

            return NoContent();
        }

        [HttpDelete("{id:minlength(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            bool isSucces = await _productRepository.DeleteAsync(id);
            if (!isSucces)
            {
                return Problem();
            }

            return NoContent();
        }
        #endregion
    }
}
