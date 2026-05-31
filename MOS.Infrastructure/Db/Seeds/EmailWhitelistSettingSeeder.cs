using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Db.Seeds
{
    public class EmailWhitelistSettingSeeder
    {
        private readonly AppDbContext _context;

        public EmailWhitelistSettingSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // TODO: check if setting already exists, skip if it does
            // TODO: create default setting with IsEnabled = false
            // TODO: save to database
        }
    }
}