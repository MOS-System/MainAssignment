using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    // EF Core IEntityTypeConfiguration per entity
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            // TODO: set table name "Tenants"
            // TODO: set primary key Id
            // TODO: Name - required, max length from ValidationConstants
            // TODO: CreatedAt - required
            // TODO: index on Name - unique
            // TODO: relationship - Tenant has many Users
        }
    }
}
