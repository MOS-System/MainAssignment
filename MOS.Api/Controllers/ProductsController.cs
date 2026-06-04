using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Services.Interfaces;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : BaseController<ProductsController>
    {
        private readonly IProductService _productService;

        public ProductsController(IConfiguration configuration, ILogger<ProductsController> logger, IProductService productService) : base(configuration, logger)
        {
            _productService = productService;
        }



        // GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        // POST api/products/favorites/{productId}
        [HttpPost("favorites/{productId}")]
        public async Task<IActionResult> AddFavorite(Guid productId)
        {
            await _productService.AddFavoriteAsync(productId);
            return NoContent();
        }

        // DELETE api/products/favorites/{productId}
        [HttpDelete("favorites/{productId}")]
        public async Task<IActionResult> RemoveFavorite(Guid productId)
        {
            await _productService.RemoveFavoriteAsync(productId);
            return NoContent();
        }
    }
}