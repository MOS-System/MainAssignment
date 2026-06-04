using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Domain.Entities
{
    // the company, contains Guid, Name, CreatedAt, Users, and other fields (add as needed)
    public class Tenant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        //Relations
        public ICollection<User> Users { get; private set; } = new List<User>();

        public Tenant(string name, string slug)
        {
            Name = name;
            Slug = slug;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            Users = new List<User>();
        }

        private Tenant() { }

        public interface ITenantSetter
        {
            Tenant? CurrentTenant { get; set; }
        }

        public interface ITenantGetter
        {
            Tenant? CurrentTenant { get; }
            string? TenantId => CurrentTenant?.Id.ToString();
        }

        public class TenantProvider : ITenantSetter, ITenantGetter
        {
            public Tenant? CurrentTenant { get; set; }

        }
    }
}
