


using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;


namespace MOS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> GetUserByLoginRequest(LoginRequest loginRequest);
        Task<AuthResponse> CreateUserByRegister(RegisterRequest registerRequest);
    }
}
