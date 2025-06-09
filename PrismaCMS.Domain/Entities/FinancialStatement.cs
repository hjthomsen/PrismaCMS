using System.Collections.Generic;
using PrismaCMS.Domain.Common;
using PrismaCMS.Domain.Enums;

namespace PrismaCMS.Domain.Entities
{
    public class FinancialStatement : BaseEntity
    {
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        public int Year { get; private set; }
        public FinancialStatementStatus Status { get; private set; }

        private readonly List<Assignment> _assignments = new();
        public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

        private FinancialStatement() { }

        public FinancialStatement(int year, Customer customer)
        {
            Year = year;
            Customer = customer;
            CustomerId = customer.Id;
            Status = FinancialStatementStatus.Draft;
        }

        public void AddAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
        }

        public void UpdateStatus(FinancialStatementStatus status)
        {
            Status = status;
        }
    }
}