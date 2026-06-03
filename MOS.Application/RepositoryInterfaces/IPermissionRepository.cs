using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<UserProductPermission>> GetPermissionByIdAsync(int userId);
        Task AddPermissionAsync(UserProductPermission permission);
        Task RemovePermissionByIdAsync(int userId);
        Task<bool> PermissionExistsAsync(int userId, int productId);
        Task<List<Product>> GetProductsByUserIdAsync(int userId);
    }
}
