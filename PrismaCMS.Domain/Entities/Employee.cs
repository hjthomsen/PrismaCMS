using System.Collections.Generic;
using PrismaCMS.Domain.Common;
using PrismaCMS.Domain.Enums;

namespace PrismaCMS.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public EmployeeRole Role { get; private set; }

        private readonly List<Assignment> _assignments = new();
        public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

        private Employee() { }

        public Employee(string name, string email, EmployeeRole role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public void UpdateRole(EmployeeRole role)
        {
            Role = role;
        }

        public void AddAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
        }
    }
}