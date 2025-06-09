using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrismaCMS.Domain.Entities;
using PrismaCMS.Domain.Enums;

namespace PrismaCMS.Infrastructure.Persistence.Configurations
{
    public class FinancialStatementConfiguration : IEntityTypeConfiguration<FinancialStatement>
    {
        public void Configure(EntityTypeBuilder<FinancialStatement> builder)
        {
            builder.HasKey(f => f.Id);

            BaseEntityConfiguration.ConfigureBaseEntity(builder);

            builder.Property(f => f.Year)
                .IsRequired();

            builder.Property(f => f.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(f => f.Customer)
                .WithMany(c => c.FinancialStatements)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.Assignments)
                .WithOne(a => a.FinancialStatement)
                .HasForeignKey(a => a.FinancialStatementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}