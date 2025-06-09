using Microsoft.EntityFrameworkCore;
using PrismaCMS.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PrismaCMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Employee> Employees { get; }
        DbSet<FinancialStatement> FinancialStatements { get; }
        DbSet<Assignment> Assignments { get; }
        DbSet<TimeEntry> TimeEntries { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}