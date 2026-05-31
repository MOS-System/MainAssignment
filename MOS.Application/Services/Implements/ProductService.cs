using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.Services.Interfaces;

namespace MOS.Application.Services.Implements
{
    // product list, favorites
    public class ProductService : BaseService<ProductService>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPermissionRepository _permissionRepository;

        public ProductService(
            IProductRepository productRepository,
            IFavoriteRepository favoriteRepository,
            IPermissionRepository permissionRepository,
            ILogger<ProductService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _productRepository = productRepository;
            _favoriteRepository = favoriteRepository;
            _permissionRepository = permissionRepository;
        }

        // TODO: GetAllAsync - takes current userId, returns List<ProductResponse>
        // mark IsFavorite based on user's favorites

        // TODO: AddFavoriteAsync - takes userId and AddFavoriteRequest
        // check product exists, check not already favorited, add

        // TODO: RemoveFavoriteAsync - takes userId and productId
        // check exists, remove
    }
}
