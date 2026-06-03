using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Responses.Users
{
    // wraps PagedResult<UserResponse>
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public RoleType Role { get; set; }

    }
}
