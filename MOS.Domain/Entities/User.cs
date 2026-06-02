using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // local user, contains password hash, status (active/inactive), and other fields (add as needed)
    public class User
    {
        public int Id { get; private set; }
        public string UserId {  get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string PasswordHash { get; private set; }

        public bool IsDeleted { get; private set; }
        public UserStatus Status { get; private set; }
        public RoleType Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        //Relations
        public int? TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }
        public ICollection<UserProductPermission> UserProductPermissions { get; private set; } = new List<UserProductPermission>();
        public ICollection<MfaCode> MfaCodes { get; private set; } = new List<MfaCode>();
        public ICollection<FavoriteService> FavoriteServices { get; private set; } = new List<FavoriteService>();
        public ICollection<AuditLog> AuditLogs { get; private set; } = new List<AuditLog>();

        public User(string name, string email, string passwordHash, string phone, string userId,
                    int? tenantId, RoleType role)
        {
            Name = name;
            Email = email;
            Phone = phone;
            UserId = userId;
            PasswordHash = passwordHash;
            IsDeleted = false;
            TenantId = tenantId;
            Role = role;
            Status = UserStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            Status = UserStatus.Inactive;
        }

        public void Reactivate()
        {
            if (Status == UserStatus.Active) return;
            Status = UserStatus.Active;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdatePhone(string phone)
        {
            Phone = phone;
        }

        public void UpdateUserId(string userId)
        {
            UserId = userId;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void ChangeRole(RoleType role)
        {
            Role = role;
        }

        private User() { }
    }
}
