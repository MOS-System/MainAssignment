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
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool IsDeleted { get; private set; }
        public UserStatus Status { get; private set; }
        public RoleType Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        //Relations
        public int TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }
        public ICollection<UserProductPermission>? UserProductPermissions { get; private set; }
        public ICollection<MfaCode>? MfaCodes { get; private set; }
        public ICollection<FavoriteService>? FavoriteServices { get; private set; }
        public ICollection<AuditLog>? AuditLogs { get; private set; }

        public User(string name, string email, string passwordHash, string passwordSalt,bool isDeleted,
                    int tenantId, RoleType role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            IsDeleted = isDeleted;
            TenantId = tenantId;
            Role = role;
            Status = UserStatus.Active;
            CreatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public void UpdateProfile(string name)
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        public void ChangePassword(string newPasswordHash)
        {
            // TODO: implement
            throw new NotImplementedException();
        }

        private User() { }
    }
}
