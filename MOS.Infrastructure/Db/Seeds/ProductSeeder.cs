using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Db.Seeds
{
    public class ProductSeeder
    {
        private readonly AppDbContext _context;

        public ProductSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // TODO: check if products already exist, skip if they do
            // TODO: create fixed product list, for example:
            // - Microsoft Teams
            // - SharePoint
            // - OneDrive
            // - Exchange
            // TODO: save to database
        }
    }
}