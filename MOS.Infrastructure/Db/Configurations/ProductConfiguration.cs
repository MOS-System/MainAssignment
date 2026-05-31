using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    // EF Core IEntityTypeConfiguration per entity
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // TODO: set table name "Products"
            // TODO: set primary key Id
            // TODO: Name - required, max length from ValidationConstants
            // TODO: Description - optional, max length 500
            // TODO: IconUrl - optional, max length 500
        }
    }
}
