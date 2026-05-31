using MOS.Application.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Repositories
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
