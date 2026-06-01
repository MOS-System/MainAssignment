using System;
using System.Collections.Generic;
using System.Text;
using MOS.Infrastructure.Db;

    namespace MOS.Infrastructure.Db.Seeds
    {
        // Master seeder - calls all other seeders in the correct order
        public class DatabaseSeeder
        {
            private readonly AppDbContext _context;

            public DatabaseSeeder(AppDbContext context)
            {
                _context = context;
            }

            public async Task SeedAsync()
            {
                // TODO: call in this order - order matters because of foreign keys
                // 1. ProductSeeder      - no dependencies
                // 2. AdminSeeder        - depends on products existing
                // 3. EmailWhitelistSettingSeeder - no dependencies
            }
        }
    }
