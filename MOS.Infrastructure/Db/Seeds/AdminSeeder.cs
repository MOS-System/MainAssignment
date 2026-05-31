using MOS.Domain.Entities;
using MOS.Infrastructure.Db;

namespace MOS.Infrastructure.Db.Seeds
{
    public class AdminSeeder
    {
        private readonly AppDbContext _context;

        public AdminSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // TODO: check if default admin already exists, skip if they do
            // TODO: create default tenant "AvePoint"
            // TODO: create default admin user
            //       - Name: "Admin"
            //       - Email: "admin@avepoint.com"
            //       - Password: hash a default password
            //       - Role: Administrator
            // TODO: save to database
        }
    }
}