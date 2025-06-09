using Microsoft.EntityFrameworkCore;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrismaCMS.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersWithFinancialStatementsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .Include(c => c.FinancialStatements)
                .ToListAsync(cancellationToken);
        }

        public async Task<Customer> GetCustomerWithFinancialStatementsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .Include(c => c.FinancialStatements)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersByYearAsync(int year, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .Where(c => c.FinancialStatements.Any(fs => fs.Year == year))
                .Include(c => c.FinancialStatements.Where(fs => fs.Year == year))
                .ToListAsync(cancellationToken);
        }
    }
}