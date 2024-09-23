using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Products.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductsController: Controller
    {

        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider) {
            _productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productsProvider.GetProductAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Products);
            }
            return NotFound();

        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId)
        {
            var result = await _productsProvider.GetProductByIdAsync(productId);
            if(result.IsSuccess)
            {
                return Ok(result.product);
            }
            return NotFound();
        }


    }
}
