using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;

namespace MOS.Infrastructure.Implements
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetByUserIdAsync
        // TODO: AddAsync
        // TODO: RemoveAsync
        // TODO: ExistsAsync
    }
}
