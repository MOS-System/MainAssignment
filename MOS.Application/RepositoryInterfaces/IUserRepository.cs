
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
        Task<(User? user, List<Product>? products)> AuthenticateUserWithProducts(LoginRequest request);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<PagedResult<User>> GetUserPagedAsync(UserQueryRequest query);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User updatedUser);
        Task DeleteUserRangeAsync(IEnumerable<int> ids);
        Task DeactivateUserRangeAsync(List<int> ids);
        Task<bool> UserExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}
