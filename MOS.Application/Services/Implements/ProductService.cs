using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOS.Application.DTOs.Requests.Products;
using MOS.Application.DTOs.Responses.Products;
using MOS.Application.Exceptions;
using MOS.Application.Services.Interfaces;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Infrastructure.Interfaces;

namespace MOS.Application.Services.Implements
{
    // product list, favorites
    public class ProductService : BaseService<ProductService>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserRepository _userRepository;
        public ProductService(
            IProductRepository productRepository,
            IFavoriteRepository favoriteRepository,
            IPermissionRepository permissionRepository,
            IUserRepository userRepository,
            ILogger<ProductService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(logger, mapper, httpContextAccessor, configuration)
        {
            _productRepository = productRepository;
            _favoriteRepository = favoriteRepository;
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var userId = GetUserIdFromJWT();
            var user = await _userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException("User", userId);

            List<Product> products;

            if (user.Role == RoleType.TenantUser)
            {
                products = await _permissionRepository.GetProductsByUserIdAsync(userId);
            }
            else
            {
                products = await _productRepository.GetAllProductAsync();
            }

            var favoriteProductIds = await _favoriteRepository.GetFavoriteIdsByUserIdAsync(userId);

            var favoriteSet = favoriteProductIds.ToHashSet();

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IconUrl = p.IconUrl,
                IsFavorite = favoriteSet.Contains(p.Id)
            }).ToList();
        }

        public async Task AddFavoriteAsync(int productId)
        {
            var userId = GetUserIdFromJWT();
            var accessibleProducts = await GetAllProductsAsync();

            if (!accessibleProducts.Any(p => p.Id == productId))
            {
                throw new NotFoundException("Product", productId);
            }

            if (await _favoriteRepository.FavoriteExistsAsync(userId, productId))
            {
                throw new ConflictException("Favorite", "already exists");
            }

            await _favoriteRepository.AddFavoriteAsync(new FavoriteService(userId, productId));
        }

        public async Task RemoveFavoriteAsync(int productId)
        {
            var userId = GetUserIdFromJWT();
            if (!(await _favoriteRepository.FavoriteExistsAsync(userId, productId)))
            {
                throw new NotFoundException("Favorite", $"{userId}-{productId}");
            }
            await _favoriteRepository.RemoveFavoriteAsync(userId, productId);
        }
    }
}
