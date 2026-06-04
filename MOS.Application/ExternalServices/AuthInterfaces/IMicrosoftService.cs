
using MOS.Application.DTOs.Responses.Auth;


namespace MOS.Application.ExternalServices.AuthInterfaces
{
    public interface IMicrosoftService
    {
        string BuildMicrosoftAuthUrl(string state);
        Task<AuthResponse> HandleMicrosoftCallbackAsync(string code);
    }
}
