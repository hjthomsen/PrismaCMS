using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Infrastructure.Persistence.Configurations
{
    public class TimeEntryConfiguration : IEntityTypeConfiguration<TimeEntry>
    {
        public void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            builder.HasKey(t => t.Id);

            BaseEntityConfiguration.ConfigureBaseEntity(builder);

            builder.Property(t => t.Date)
                .IsRequired();

            builder.Property(t => t.HoursWorked)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.HasOne(t => t.Assignment)
                .WithMany(a => a.TimeEntries)
                .HasForeignKey(t => t.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}