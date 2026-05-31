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
            // TODO: set table name "EmailWhitelists"
            // TODO: set primary key Id
            // TODO: Email - required, max length from ValidationConstants
            // TODO: AddedAt - required
            // TODO: index on Email - unique
        }
    }
}
