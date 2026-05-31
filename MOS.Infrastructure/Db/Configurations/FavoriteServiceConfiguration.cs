using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Infrastructure.Db.Configurations
{
    public class FavoriteServiceConfiguration : IEntityTypeConfiguration<FavoriteService>
    {
        public void Configure(EntityTypeBuilder<FavoriteService> builder)
        {
            // TODO: set table name "FavoriteServices"
            // TODO: set primary key Id
            // TODO: AddedAt - required
            // TODO: index on UserId - for fetching user's favorites
            // TODO: index on UserId + ProductId - unique (no duplicate favorites)
            // TODO: relationship - belongs to User (many to one)
            // TODO: relationship - belongs to Product (many to one)
        }
    }
}
