using Microsoft.EntityFrameworkCore;
using MOS.Domain.Entities;
using MOS.Infrastructure.Db;
using MOS.Infrastructure.Interfaces;

namespace MOS.Infrastructure.Implements
{
    public class EmailWhitelistRepository : IEmailWhitelistRepository
    {
        private readonly AppDbContext _context;

        public EmailWhitelistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmailWhitelistSetting?> GetSettingAsync()
        {
            return await _context.EmailWhitelistSettings
                .FirstOrDefaultAsync();
        }

        public async Task AddSettingAsync(EmailWhitelistSetting setting)
        {
            await _context.EmailWhitelistSettings.AddAsync(setting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettingAsync(EmailWhitelistSetting setting)
        {
            _context.EmailWhitelistSettings.Update(setting);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmailWhitelist>> GetEmailsAsync()
        {
            return await _context.EmailWhitelists
                .OrderByDescending(e => e.AddedAt)
                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLower();

            return await _context.EmailWhitelists
                .AnyAsync(e => e.Email.ToLower() == normalizedEmail);
        }

        public async Task AddEmailAsync(EmailWhitelist email)
        {
            await _context.EmailWhitelists.AddAsync(email);
            await _context.SaveChangesAsync();
        }

        public async Task<EmailWhitelist?> GetEmailByIdAsync(int id)
        {
            return await _context.EmailWhitelists
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task RemoveEmailAsync(EmailWhitelist email)
        {
            _context.EmailWhitelists.Remove(email);
            await _context.SaveChangesAsync();
        }
    }
}