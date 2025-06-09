using System.Collections.Generic;
using PrismaCMS.Domain.Common;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public int FinancialStatementId { get; private set; }
        public FinancialStatement FinancialStatement { get; private set; } = null!;

        public int EmployeeId { get; private set; }
        public Employee Employee { get; private set; } = null!;

        public double AllocatedHours { get; private set; }

        private readonly List<TimeEntry> _timeEntries = new();
        public IReadOnlyCollection<TimeEntry> TimeEntries => _timeEntries.AsReadOnly();

        private Assignment() { }

        public Assignment(FinancialStatement financialStatement, Employee employee, double allocatedHours)
        {
            FinancialStatement = financialStatement;
            FinancialStatementId = financialStatement.Id;
            Employee = employee;
            EmployeeId = employee.Id;
            AllocatedHours = allocatedHours;
        }

        public void UpdateAllocatedHours(double hours)
        {
            AllocatedHours = hours;
        }

        public void AddTimeEntry(TimeEntry timeEntry)
        {
            _timeEntries.Add(timeEntry);
        }
    }
}