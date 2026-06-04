using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Responses.Users
{
    // wraps PagedResult<UserResponse>
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public SigninMethod SigninMethod { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;
        public RoleType Role { get; set; } 

    }
}
