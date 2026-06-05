using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Domain.Entities;

namespace MOS.Infrastructure.Db.Configurations
{
    // index on UserId + Expriry
    public class MfaCodeConfiguration : IEntityTypeConfiguration<MfaCode>
    {
        public void Configure(EntityTypeBuilder<MfaCode> builder)
        {
            //builder.ToTable("MfaCodes");
            //         builder.HasKey(e => e.Id);
            //
            //         builder.Property(e => e.Code)
            //             .IsRequired()
            //             .HasMaxLength(6);
            //
            //         builder.Property(e => e.IsUsed)
            //             .HasDefaultValue(false);
            //
            //         builder.Property(e => e.CreatedAt)
            //             .HasDefaultValueSql("GETUTCDATE()");
            //
            //         // Composite index for ValidCode query
            //         builder.HasIndex(e => new { e.UserId, e.IsUsed, e.ExpiresAt })
            //             .HasDatabaseName("IX_MfaCodes_UserId_IsUsed_ExpiresAt");
            //
            //         builder.HasOne(e => e.User)
            //             .WithMany(u => u.MfaCodes)
            //             .HasForeignKey(e => e.UserId)
            //             .OnDelete(DeleteBehavior.Cascade)
            //             .HasConstraintName("FK_MfaCodes_Users");
        }
    }
}
