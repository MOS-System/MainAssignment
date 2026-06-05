


using MOS.Application.DTOs.Requests.Auth;
using MOS.Application.DTOs.Responses.Auth;


namespace MOS.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserWithProducts(RegisterRequest registerRequest);
     
    }
}
