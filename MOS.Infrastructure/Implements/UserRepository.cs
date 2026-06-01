
using MOS.Infrastructure.Db;
using MOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MOS.Application.Common;
using MOS.Infrastructure.Interfaces;
using MOS.Application.DTOs.Requests.Users;
using MOS.Application.Services.Interfaces;

namespace MOS.Infrastructure.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // TODO: GetPagedAsync - pagination, sorting, search, filter

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateUserRangeAsync(List<int> ids)
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

        public async Task DeleteUserRangeAsync(IEnumerable<int> ids)
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

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<PagedResult<User>> GetUserPagedAsync(UserQueryRequest query)
        {
            // Step 1 — start with base query
            // IQueryable means nothing hits the DB yet
            // we're just building the query
            var queryable = _context.Users
                .Where(u => !u.IsDeleted);

            // Step 2 — apply search
            // searches both name and email
            if (!string.IsNullOrEmpty(query.Search))
            {
                var search = query.Search.ToLower();
                queryable = queryable.Where(u =>
                    u.Name.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search));
            }

            // Step 3 — apply filters
            if (query.StatusFilter.HasValue)
                queryable = queryable.Where(u => u.Status == query.StatusFilter.Value);

            if (query.RoleFilter.HasValue)
                queryable = queryable.Where(u => u.Role == query.RoleFilter.Value);

            // Step 4 — get total count BEFORE pagination
            // important — count after filters but before Skip/Take
            var totalCount = await queryable.CountAsync();

            // Step 5 — apply sorting
            queryable = query.SortBy?.ToLower() switch
            {
                "email" => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Email)
                    : queryable.OrderBy(u => u.Email),
                "status" => query.SortDirection?.ToLower() == "desc"
                    ? queryable.OrderByDescending(u => u.Status)
                    : queryable.OrderBy(u => u.Status),
                _ => query.SortDirection?.ToLower() == "desc"  // default sort by name
                    ? queryable.OrderByDescending(u => u.Name)
                    : queryable.OrderBy(u => u.Name)
            };

            // Step 6 — apply pagination
            // Skip jumps past previous pages
            // Take limits how many records we get
            var items = await queryable
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            // Step 7 — return wrapped in PagedResult
            return new PagedResult<User>
            {
                Items = items,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower() && !u.IsDeleted);
        }

    }
}
