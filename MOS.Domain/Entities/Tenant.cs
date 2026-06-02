using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // the company, contains Guid, Name, CreatedAt, Users, and other fields (add as needed)
    public class Tenant
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        //Relations
        public EmailWhitelistSetting? EmailWhitelistSetting { get; private set; }
        public ICollection<EmailWhitelist>? EmailWhitelist { get; private set; }
        public ICollection<User>? Users { get; private set; }

        public Tenant(string name, string slug)
        {
            Name = name;
            Slug = slug;
            CreatedAt = DateTime.UtcNow;
            Users = new List<User>();
            IsActive = true;
        }

        private Tenant() { }
    }
}
