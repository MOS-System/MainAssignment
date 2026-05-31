using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // stores allowed email addresses
    public class EmailWhitelist
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public DateTime AddedAt { get; private set; }
        public int AddedBy { get; private set; }

        //Realtions
        public int TenantId { get; private set; }
        public Tenant? Tenant { get; private set; }

        public EmailWhitelist(string email, int addedBy, int tenantId)
        {
            Email = email;
            AddedAt = DateTime.UtcNow;
            AddedBy = addedBy;
            TenantId = tenantId;
        }

        private EmailWhitelist() { }
    }
}
