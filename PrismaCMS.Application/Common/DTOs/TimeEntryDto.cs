namespace PrismaCMS.Application.Common.DTOs
{
    public class TimeEntryDto
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public AssignmentDto? Assignment { get; set; }
        public DateTime Date { get; set; }
        public double HoursWorked { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    public record TimeEntryCreateDto(
        int AssignmentId,
        DateTime Date,
        double HoursWorked,
        string Description
    );

    public record TimeEntryUpdateDto(
        DateTime Date,
        double HoursWorked,
        string Description
    );
}