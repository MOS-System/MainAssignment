
using MOS.Infrastructure.Db;
using MOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.Services.Interfaces;
using MOS.Application.DTOs.Requests.Auth;
using MOS.Domain.Enums;


namespace MOS.Infrastructure.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;

        public UserRepository(AppDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }


        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateUserRangeAsync(List<Guid> ids)
        {
            var users = await _context.Users
                             .Where(u => ids.Contains(u.Id) && !u.IsDeleted)
                             .ToListAsync();
            foreach (var user in users)
            {
                user.Deactivate();
            }

            await _context.SaveChangesAsync();
        }

        public async Task ReactivateUserRangeAsync(List<Guid> ids)
        {
            var users = await _context.Users
                              .Where(u => ids.Contains(u.Id) && !u.IsDeleted)
                              .ToListAsync();
            foreach (var user in users)
            {
                user.Reactivate();
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserRangeAsync(IEnumerable<Guid> ids)
        {
            var users = await _context.Users
                            .Where(u => ids.Contains(u.Id) && !u.IsDeleted)
                            .ToListAsync();
            foreach (var user in users)
            {
                user.Delete();
            }

            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
           .Include(u => u.UserProductPermissions)!
           .ThenInclude(p => p.Product)
           .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted)!;
        }

        public async Task<PagedResult<User>> GetUserPagedAsync(UserQueryRequest query)
        {
            var page = query.Page <= 0 ? 1 : query.Page;
            var pageSize = query.PageSize <= 0 ? 10 : query.PageSize;

            var queryable = _context.Users
                .Where(u => !u.IsDeleted);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var search = query.Search.ToLower();

                queryable = queryable.Where(u =>
                    u.Name.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search));
            }

            if (query.StatusFilter.HasValue)
                queryable = queryable.Where(u => u.Status == query.StatusFilter.Value);

            if (query.RoleFilter.HasValue)
                queryable = queryable.Where(u => u.Role == query.RoleFilter.Value);

            var totalCount = await queryable.CountAsync();
            queryable = query.SortBy?.ToLower() switch
            {
                "name" => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Name)
                    : queryable.OrderBy(u => u.Name),

                "email" => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Email)
                    : queryable.OrderBy(u => u.Email),

                "status" => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Status)
                    : queryable.OrderBy(u => u.Status),

                "role" => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Role)
                    : queryable.OrderBy(u => u.Role),

                _ => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Id)
                    : queryable.OrderBy(u => u.Id)
            };

            var items = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<User>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);
        }

        public async Task<(User? user, List<Product>? products)> AuthenticateUserWithProducts(LoginRequest request)
        {
            var user = await GetUserByEmailAsync(request.Email);

            if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
                return (null, null);

            List<Product?> products;

            if (user.Role == RoleType.Administrator)
            { 
                products = await _context.UserProductPermissions
                    .Include(upp => upp.Product)
                    .Select(upp => upp.Product)
                    .Distinct()
                    .ToListAsync();
            }
            else
            {
                products = await _context.UserProductPermissions
                    .Where(upp => upp.UserId == user.Id)
                    .Include(upp => upp.Product)
                    .Select(upp => upp.Product)
                    .ToListAsync();
            }

            return (user, products)!;
        }


    }
}
