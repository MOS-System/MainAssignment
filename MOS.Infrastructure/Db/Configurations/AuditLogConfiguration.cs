using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    // index on Action, Timestamp, UserId
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            // TODO: set table name "AuditLogs"
            // TODO: set primary key Id
            // TODO: UserName - required, max length from ValidationConstants
            // TODO: Action - required, store as string
            // TODO: ObjectAffected - required, max length 500
            // TODO: Timestamp - required
            // TODO: index on Timestamp - for sorting audit records
            // TODO: index on UserId - for filtering by user
            // TODO: relationship - belongs to User (many to one)
        }
    }
}
