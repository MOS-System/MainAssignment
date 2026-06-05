
namespace MOS.Application.DTOs.Requests.Auth
{
    public class VerifyRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MfaCode { get; set; } = string.Empty;
    }
}
