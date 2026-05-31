using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    public class UserProductPermissionConfiguration
        : IEntityTypeConfiguration<UserProductPermission>
    {
        public void Configure(EntityTypeBuilder<UserProductPermission> builder)
        {
            // TODO: set table name "UserProductPermissions"
            // TODO: set primary key Id
            // TODO: index on UserId - for fetching user's permissions
            // TODO: index on UserId + ProductId - unique (no duplicate permissions)
            // TODO: relationship - belongs to User (many to one)
            // TODO: relationship - belongs to Product (many to one)
        }
    }
}
