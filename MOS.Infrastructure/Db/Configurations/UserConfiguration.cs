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
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
              .IsRequired()
              .HasMaxLength(10);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Phone)
               .IsRequired()
               .HasMaxLength(10);

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(e => e.Status)
                .HasConversion<int>();

            builder.Property(e => e.Role)
                .HasConversion<int>();

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Email unique per tenant
            builder.HasIndex(e => new { e.TenantId, e.Email })
                .IsUnique()
                .HasDatabaseName("UX_Users_TenantId_Email");

            // Performance index for listing/filtering
            builder.HasIndex(e => new { e.TenantId, e.IsDeleted })
                .HasDatabaseName("IX_Users_TenantId_IsDeleted");

            // Soft-delete global query filter
            builder.HasQueryFilter(e => !e.IsDeleted);

            // User → Tenant (many-to-one)
            builder.HasOne(e => e.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(e => e.TenantId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Users_Tenants");
        }
    }
}
