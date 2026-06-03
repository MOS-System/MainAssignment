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
                .IsRequired();
        }
    }
}
