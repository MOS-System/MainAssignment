using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<FavoriteService>> GetFavoritesByUserIdAsync(Guid userId);
        Task<List<Guid>> GetFavoriteIdsByUserIdAsync(Guid userId);
        Task AddFavoriteAsync(FavoriteService fav);
        Task RemoveFavoriteAsync(Guid userId, Guid productId);
        Task<bool> FavoriteExistsAsync(Guid userId, Guid productId);
    }
}
