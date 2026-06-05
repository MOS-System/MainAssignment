
using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Requests.Users;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Application.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<(User? user, List<Product>? products)> AuthenticateUserWithProducts(VerifyRequest request);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<PagedResult<User>> GetUserPagedAsync(UserQueryRequest query);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User updatedUser);
        Task DeleteUserRangeAsync(IEnumerable<Guid> ids);
        Task DeactivateUserRangeAsync(List<Guid> ids);
        Task ReactivateUserRangeAsync(List<Guid> ids);
        Task<bool> UserExistsAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
    }
}
