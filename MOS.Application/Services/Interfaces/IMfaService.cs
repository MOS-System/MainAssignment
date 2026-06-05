
using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;

namespace MOS.Application.Services.Interfaces
{
    public interface IMfaService
    {
        Task<string> GetMfaCode(LoginRequest loginRequest);
        Task<AuthResponse> VerifyMfaCodeAndAuthUserWithProduct(VerifyRequest verifyRequest);
    }
}
