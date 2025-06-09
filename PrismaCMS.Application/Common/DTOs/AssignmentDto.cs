namespace PrismaCMS.Application.Common.DTOs
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public int FinancialStatementId { get; set; }
        public FinancialStatementDto? FinancialStatement { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }
        public double AllocatedHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public List<TimeEntryDto> TimeEntries { get; set; } = new();
    }

    public record AssignmentCreateDto(
        int FinancialStatementId,
        int EmployeeId,
        double AllocatedHours
    );

    public record AssignmentUpdateDto(
        double AllocatedHours
    );
}