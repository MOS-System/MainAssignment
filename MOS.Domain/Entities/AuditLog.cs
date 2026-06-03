using MOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // stores all sign-in/out, add/update actions
    public class AuditLog
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Category { get; private set; }
        public string Email { get; private set; }
        public AuditAction Action { get; private set; }
        public string ObjectAffected { get; private set; }
        public DateTime Timestamp { get; private set; }

        //Relations
        public int? UserId { get; private set; }
        public User? User { get; private set; }
        public AuditLog(int? userId, string name, string userName, string category,
            string email, AuditAction action, string objectAffected)
        {
            UserId = userId;
            Name = name;
            UserName = userName;
            Category = category;
            Email = email;
            Action = action;
            ObjectAffected = objectAffected;
            Timestamp = DateTime.UtcNow;
        }

        private AuditLog() { }
    }
}
