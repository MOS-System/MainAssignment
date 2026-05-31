using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    public class EmailWhitelistConfiguration : IEntityTypeConfiguration<EmailWhitelist>
    {
        public void Configure(EntityTypeBuilder<EmailWhitelist> builder)
        {
            builder.ToTable("EmailWhitelists");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.AddedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Same email can't appear twice in same tenant's list
            builder.HasIndex(e => new { e.TenantId, e.Email })
                .IsUnique()
                .HasDatabaseName("UX_EmailWhitelists_TenantId_Email");

            // EmailWhitelist → Tenant (many-to-one)
            builder.HasOne(e => e.Tenant)
                .WithMany(t => t.EmailWhitelist)
                .HasForeignKey(e => e.TenantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EmailWhitelists_Tenants");
        }
    }
}
