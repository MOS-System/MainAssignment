using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;

namespace MOS.Infrastructure.Db.Configurations
{
    public class UserProductPermissionConfiguration
        : IEntityTypeConfiguration<UserProductPermission>
    {
        public void Configure(EntityTypeBuilder<UserProductPermission> builder)
        {
            builder.ToTable("UserProductPermissions");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PermissionLevel)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(e => e.AssignedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // One user = one permission row per product
            builder.HasIndex(e => new { e.UserId, e.ProductId })
                .IsUnique()
                .HasDatabaseName("UX_UserProductPermissions_UserId_ProductId");

            // UserProductPermission → User
            builder.HasOne(e => e.User)
                .WithMany(u => u.UserProductPermissions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserProductPermissions_Users");

            // UserProductPermission → Product
            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserProductPermissions_Products");
        }
    }
}
