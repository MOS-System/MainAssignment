using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MOS.Infrastructure.Implements
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(FavoriteService fav)
        {
            await _context.FavoriteServices.AddAsync(fav);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> FavoriteExistsAsync(Guid userId, Guid productId)
        {
            return await _context.FavoriteServices.AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }

        public async Task RemoveFavoriteAsync(Guid userId, Guid productId)
        {
            var favorite = await _context.FavoriteServices
                                    .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            _context.FavoriteServices.Remove(favorite);

            await _context.SaveChangesAsync();
        }

        public async Task<List<FavoriteService>> GetFavoritesByUserIdAsync(Guid id)
        {
            return await _context.FavoriteServices
                            .Include(f => f.Product)
                            .Where(f => f.UserId == id)
                            .ToListAsync();
        }

        public async Task<List<Guid>> GetFavoriteIdsByUserIdAsync(Guid id)
        {
            return await _context.FavoriteServices
                            .Where(f => f.UserId == id)
                            .Select(f => f.ProductId)
                            .ToListAsync();
        }
    }
}
