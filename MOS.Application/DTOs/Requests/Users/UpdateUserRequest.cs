using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Requests.Users
{
    public class UpdateUserRequest
    {
        public string? Name { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public RoleType? Role { get; set; }
        public List<Guid>? ProductIds { get; set; } = new();
    }

}
