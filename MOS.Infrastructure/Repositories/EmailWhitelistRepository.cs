using MOS.Application.Interfaces;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Repositories
{
    public class EmailWhitelistRepository : IEmailWhitelistRepository
    {
        private readonly AppDbContext _context;

        public EmailWhitelistRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetAllAsync
        // TODO: GetSettingAsync
        // TODO: AddAsync
        // TODO: RemoveAsync
        // TODO: IsAllowedAsync
        // TODO: UpdateSettingAsync
    }
}
