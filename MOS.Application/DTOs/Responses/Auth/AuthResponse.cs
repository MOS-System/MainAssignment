using MOS.Application.DTOs.Responses.Products;
using MOS.Application.DTOs.Responses.Users;


namespace MOS.Application.DTOs.Responses.Auth
{
    public class AuthResponse : UserResponse
    {
        public string Token { get; set; } = string.Empty;
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
    }
}
