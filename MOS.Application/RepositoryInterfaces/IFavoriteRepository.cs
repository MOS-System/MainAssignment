using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<FavoriteService>> GetFavoritesByUserIdAsync(int userId);
        Task<List<int>> GetFavoriteIdsByUserIdAsync(int userId);
        Task AddFavoriteAsync(FavoriteService fav);
        Task RemoveFavoriteAsync(int userId, int productId);
        Task<bool> FavoriteExistsAsync(int userId, int productId);
    }
}
