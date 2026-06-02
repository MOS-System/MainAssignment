using MOS.Application.Common;
using MOS.Application.DTOs.Requests.Audit;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AuditRepository : IAuditRepository
{
    private readonly AppDbContext _context;

    public AuditRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AuditLog log)
    {
        await _context.AuditLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<AuditLog>> GetPagedAsync(AuditQueryRequest query)
    {
        var queryable = _context.AuditLogs.AsQueryable();

        // search by object, name, userId
        if (!string.IsNullOrEmpty(query.Search))
        {
            var search = query.Search.ToLower();
            queryable = queryable.Where(a =>
                a.UserName.ToLower().Contains(search) ||
                a.ObjectAffected.ToLower().Contains(search) ||
                a.UserId.ToString().Contains(search));
        }

        var totalCount = await queryable.CountAsync();

        var items = await queryable
            .OrderByDescending(a => a.Timestamp)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedResult<AuditLog>
        {
            Items = items,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}