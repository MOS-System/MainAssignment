using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.DTOs.Requests.Products;
using MOS.Application.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    // GET api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // TODO: get current userId from JWT claims
        // TODO: call _productService.GetAllAsync
        // TODO: return 200 with List<ProductResponse>
        throw new NotImplementedException();
    }

    // POST api/products/favorites
    [HttpPost("favorites")]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteProductRequest request)
    {
        // TODO: get current userId from JWT claims
        // TODO: call _productService.AddFavoriteAsync
        // TODO: return 201
        throw new NotImplementedException();
    }

    // DELETE api/products/favorites/{productId}
    [HttpDelete("favorites/{productId}")]
    public async Task<IActionResult> RemoveFavorite(int productId)
    {
        // TODO: get current userId from JWT claims
        // TODO: call _productService.RemoveFavoriteAsync
        // TODO: return 204
        throw new NotImplementedException();
    }
}
