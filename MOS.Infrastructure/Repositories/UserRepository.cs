using MOS.Application.DTOs.Requests.Users;
using MOS.Application.Common;
using MOS.Application.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Repositories
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
