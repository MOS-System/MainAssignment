

namespace MOS.Application.DTOs.Requests.Mfa
{
    public class MicrosoftAuthRequest
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
    }
}
