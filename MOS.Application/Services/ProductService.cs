using MOS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Services
{
    // product list, favorites
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPermissionRepository _permissionRepository;

        public ProductService(
            IProductRepository productRepository,
            IFavoriteRepository favoriteRepository,
            IPermissionRepository permissionRepository)
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
