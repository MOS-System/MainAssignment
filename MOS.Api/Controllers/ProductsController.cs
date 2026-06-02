using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOS.Api.Controllers;
using MOS.Application.DTOs.Requests.Products;
using MOS.Application.Services.Implements;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Constants;
using MOS.Domain.Enums;


[Authorize]
public class ProductsController : BaseController<ProductsController>
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger) : base(logger)
    {
        _productService = productService;
    }



    // GET api/products
    [HttpGet]
    [Authorize(Policy = Permissions.AdminPolicy)]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
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
