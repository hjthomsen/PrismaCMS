using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Infrastructure.Persistence.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.Id);

            BaseEntityConfiguration.ConfigureBaseEntity(builder);

            builder.Property(a => a.AllocatedHours)
                .IsRequired();

            builder.HasOne(a => a.FinancialStatement)
                .WithMany(f => f.Assignments)
                .HasForeignKey(a => a.FinancialStatementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Employee)
                .WithMany(e => e.Assignments)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.TimeEntries)
                .WithOne(t => t.Assignment)
                .HasForeignKey(t => t.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}