


using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;
using MOS.Application.DTOs.Responses.Mfa;


namespace MOS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateUserWithProducts(LoginRequest loginRequest);
        Task<AuthResponse> RegisterUserWithProducts(RegisterRequest registerRequest);
     
    }
}
