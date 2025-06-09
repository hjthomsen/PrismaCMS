using System;
using PrismaCMS.Domain.Common;

namespace PrismaCMS.Domain.Entities
{
    public class TimeEntry : BaseEntity
    {
        public int AssignmentId { get; private set; }
        public Assignment Assignment { get; private set; } = null!;

        public DateTime Date { get; private set; }
        public double HoursWorked { get; private set; }
        public string Description { get; private set; } = string.Empty;

        private TimeEntry() { }

        public TimeEntry(Assignment assignment, DateTime date, double hoursWorked, string description)
        {
            Assignment = assignment;
            AssignmentId = assignment.Id;
            Date = date;
            HoursWorked = hoursWorked;
            Description = description;
        }

        public void UpdateHours(double hours)
        {
            if (hours < 0)
                throw new ArgumentException("Hours worked cannot be negative", nameof(hours));

            HoursWorked = hours;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
    }
}