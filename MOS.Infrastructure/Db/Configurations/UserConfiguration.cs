using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using MOS.Domain.Entities;
using MOS.Domain.Enums;
using MOS.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MOS.Infrastructure.Db.Configurations
{
    // EF Core IEntityTypeConfiguration per entity -> indexes on Email, Name, etc for search/sort
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // TODO: set table name "Users"
            // TODO: set primary key Id
            // TODO: Name - required, max length from ValidationConstants
            // TODO: Email - required, max length from ValidationConstants
            // TODO: PasswordHash - required
            // TODO: Status - required, store as string
            // TODO: Role - required, store as string
            // TODO: CreatedAt - required
            // TODO: index on Email - unique
            // TODO: index on Name - for sorting/search performance
            // TODO: index on TenantId - for filtering by tenant
            // TODO: relationship - User belongs to Tenant (many to one)
        }
    }
}
