using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            BaseEntityConfiguration.ConfigureBaseEntity(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.OrgNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.OwnsOne(c => c.ContactInfo, contactBuilder =>
            {
                contactBuilder.Property(c => c.Email)
                    .HasMaxLength(100);

                contactBuilder.Property(c => c.Phone)
                    .HasMaxLength(20);

                contactBuilder.Property(c => c.Address)
                    .HasMaxLength(200);

                contactBuilder.Property(c => c.City)
                    .HasMaxLength(50);

                contactBuilder.Property(c => c.PostalCode)
                    .HasMaxLength(20);

                contactBuilder.Property(c => c.Country)
                    .HasMaxLength(50);
            });

            builder.HasMany(c => c.FinancialStatements)
                .WithOne(f => f.Customer)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}