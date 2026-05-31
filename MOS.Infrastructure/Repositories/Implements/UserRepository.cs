
using MOS.Infrastructure.Db;
using MOS.Application.Services.Interfaces;

namespace MOS.Infrastructure.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetByIdAsync
        // TODO: GetByEmailAsync
        // TODO: GetPagedAsync - pagination, sorting, search, filter
        // TODO: AddAsync
        // TODO: UpdateAsync
        // TODO: DeleteRangeAsync
        // TODO: DeactivateRangeAsync
    }
}
