using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrismaCMS.Domain.Common;

namespace PrismaCMS.Infrastructure.Persistence.Configurations
{
    public static class BaseEntityConfiguration
    {
        public static void ConfigureBaseEntity<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(100);

            builder.Property(e => e.LastModifiedAt);

            builder.Property(e => e.LastModifiedBy)
                .HasMaxLength(100);
        }
    }
}