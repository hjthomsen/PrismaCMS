using Microsoft.EntityFrameworkCore;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;
using System.Reflection;

namespace PrismaCMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<FinancialStatement> FinancialStatements => Set<FinancialStatement>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply configurations from the current assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}