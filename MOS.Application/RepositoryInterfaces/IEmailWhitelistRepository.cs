using MOS.Domain.Entities;

namespace MOS.Infrastructure.Interfaces
{
    public interface IEmailWhitelistRepository
    {
        // setting methods
        Task<EmailWhitelistSetting?> GetSettingAsync();
        Task AddSettingAsync(EmailWhitelistSetting setting);
        Task UpdateSettingAsync(EmailWhitelistSetting setting);

        // whitelist-related method
        Task<List<EmailWhitelist>> GetEmailsAsync();
        Task<bool> EmailExistsAsync(string email);
        Task AddEmailAsync(EmailWhitelist email);
        Task<EmailWhitelist?> GetEmailByIdAsync(Guid id);
        Task RemoveEmailAsync(EmailWhitelist email);
    }
}