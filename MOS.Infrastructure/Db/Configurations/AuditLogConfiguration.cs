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
            builder.ToTable("AuditLogs");
            builder.HasKey(e => e.Id);

            // Snapshot fields
            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(200)
                .HasDefaultValue("");

            builder.Property(e => e.UserEmail)
                .IsRequired()
                .HasMaxLength(256)
                .HasDefaultValue("");

            builder.Property(e => e.Action)
                .HasConversion<string>()
                .HasMaxLength(100);

            builder.Property(e => e.ObjectAffected)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Timestamp)
                .HasDefaultValueSql("GETUTCDATE()");

            // Most queries order by time
            builder.HasIndex(e => e.Timestamp)
                .HasDatabaseName("IX_AuditLogs_Timestamp");

            // Search by name in audit page
            builder.HasIndex(e => e.UserName)
                .HasDatabaseName("IX_AuditLogs_UserName");

            // AuditLog → User (SET NULL — log survives user deletion)
            builder.HasOne(e => e.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull)   // ← NOT Cascade
                .HasConstraintName("FK_AuditLogs_Users");
        }
    }
}
