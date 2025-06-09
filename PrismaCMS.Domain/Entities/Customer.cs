using System.Collections.Generic;
using PrismaCMS.Domain.Common;
using PrismaCMS.Domain.ValueObjects;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string OrgNumber { get; private set; } = string.Empty;
        public ContactInfo ContactInfo { get; private set; } = null!;

        private readonly List<FinancialStatement> _financialStatements = new();
        public IReadOnlyCollection<FinancialStatement> FinancialStatements => _financialStatements.AsReadOnly();

        private Customer() { }

        public Customer(string name, string orgNumber, ContactInfo contactInfo)
        {
            Name = name;
            OrgNumber = orgNumber;
            ContactInfo = contactInfo;
        }

        public void UpdateContactInfo(ContactInfo contactInfo)
        {
            ContactInfo = contactInfo;
        }

        public void AddFinancialStatement(FinancialStatement financialStatement)
        {
            _financialStatements.Add(financialStatement);
        }
    }
}