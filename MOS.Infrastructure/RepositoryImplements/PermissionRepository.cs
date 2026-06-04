using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MOS.Infrastructure.Implements
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;

        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProductPermission>> GetPermissionByIdAsync(Guid userId) => await _context.UserProductPermissions
                .Where(p => p.UserId == userId)
                .ToListAsync();

        public async Task AddPermissionAsync(UserProductPermission permission)
        {
            await _context.UserProductPermissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePermissionByIdAsync(Guid userId)
        {
            var permissions = await _context.UserProductPermissions
                .Where(p => p.UserId == userId)
                .ToListAsync();

            _context.UserProductPermissions.RemoveRange(permissions);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PermissionExistsAsync(Guid userId, Guid productId)
        {
            return await _context.UserProductPermissions
                .AnyAsync(p => p.UserId == userId
                            && p.ProductId == productId);
        }

        public async Task<List<Product>> GetProductsByUserIdAsync(Guid userId)
        {
            return await _context.UserProductPermissions
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .Select(p => p.Product!)
                .ToListAsync();
        }

        public async Task AddPermissionsAsync(List<UserProductPermission> permissions)
        {
            await _context.UserProductPermissions.AddRangeAsync(permissions);
            await _context.SaveChangesAsync();
        }
    }
}