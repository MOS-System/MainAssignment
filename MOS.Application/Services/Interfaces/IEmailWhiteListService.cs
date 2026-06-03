using MOS.Application.DTOs.Requests.EmailWhitelist;
using MOS.Application.DTOs.Responses.EmailWhitelist;

namespace MOS.Application.Services.Interfaces
{
    public interface IEmailWhitelistService
    {
        Task<EmailWhitelistResponse> GetWhitelistAsync();

        Task UpdateSettingAsync(UpdateEmailWhitelistSettingRequest request);

        Task AddEmailAsync(AddEmailWhitelistRequest request);

        Task RemoveEmailAsync(int id);

        Task<bool> IsAllowedAsync(string email);
    }
}
