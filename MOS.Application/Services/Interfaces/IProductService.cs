using MOS.Application.DTOs.Responses.Products;

namespace MOS.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task AddFavoriteAsync(Guid productId);
        Task RemoveFavoriteAsync(Guid productId);
    }
}
