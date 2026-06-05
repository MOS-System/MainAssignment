using Microsoft.EntityFrameworkCore;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db.Configurations;

namespace MOS.Infrastructure.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // TODO: add DbSet for each entity
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FavoriteService> FavoriteServices { get; set; }
        public DbSet<UserProductPermission> UserProductPermissions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<EmailWhitelist> EmailWhitelists { get; set; }
        public DbSet<EmailWhitelistSetting> EmailWhitelistSettings { get; set; }
        public DbSet<MfaCode> MfaCodes { get; set; } // bonus

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(TenantConfiguration).Assembly);

            // TODO: apply all configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteServiceConfiguration());
            modelBuilder.ApplyConfiguration(new UserProductPermissionConfiguration());
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ApplyConfiguration(new EmailWhitelistConfiguration());
            modelBuilder.ApplyConfiguration(new EmailWhitelistSettingConfiguration());
            modelBuilder.ApplyConfiguration(new MfaCodeConfiguration()); 
        }
    }
}