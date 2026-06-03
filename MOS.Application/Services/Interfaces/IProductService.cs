using MOS.Application.DTOs.Responses.Products;

namespace MOS.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task AddFavoriteAsync(int productId);
        Task RemoveFavoriteAsync(int productId);
    }
}
