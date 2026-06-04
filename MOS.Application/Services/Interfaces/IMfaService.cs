
using MOS.Application.DTOs.Requests.Mfa;

namespace MOS.Application.Services.Interfaces
{
    public interface IMfaService
    {
        string BuildMicrosoftAuthUrl(string state);
        Task<MicrosoftAuthRequest> HandleMicrosoftCallbackAsync(string code);
    }
}
