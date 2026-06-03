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
            builder.ToTable("FavoriteServices");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.AddedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // One user can only favorite a product once
            builder.HasIndex(e => new { e.UserId, e.ProductId })
                .IsUnique()
                .HasDatabaseName("UX_FavoriteServices_UserId_ProductId");

            // FavoriteService → User
            builder.HasOne(e => e.User)
                .WithMany(u => u.FavoriteServices)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FavoriteServices_Users");

            // FavoriteService → Product
            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_FavoriteServices_Products");
        }
    }
}
