using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<UserProductPermission>> GetPermissionByIdAsync(Guid userId);
        Task AddPermissionAsync(UserProductPermission permission);
        Task RemovePermissionByIdAsync(Guid userId);
        Task<bool> PermissionExistsAsync(Guid userId, Guid productId);
        Task<List<Product>> GetProductsByUserIdAsync(Guid userId);
    }
}
