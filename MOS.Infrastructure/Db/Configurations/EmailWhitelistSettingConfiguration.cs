using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    public class EmailWhitelistSettingConfiguration
        : IEntityTypeConfiguration<EmailWhitelistSetting>
    {
        public void Configure(EntityTypeBuilder<EmailWhitelistSetting> builder)
        {
            builder.ToTable("EmailWhitelistSettings");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsEnabled)
                .HasDefaultValue(false);

            // One setting row per tenant
            builder.HasIndex(e => e.UserId)
                .IsUnique()
                .HasDatabaseName("UX_EmailWhitelistSettings_UserId");

            // FK lives here — dependent side of one-to-one
            builder.HasOne(e => e.User)
                .WithOne(t => t.EmailWhitelistSetting)
                .HasForeignKey<EmailWhitelistSetting>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EmailWhitelistSettings_Users");
        }
    }
}
