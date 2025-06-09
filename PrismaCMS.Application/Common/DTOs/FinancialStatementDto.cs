using PrismaCMS.Domain.Enums;

namespace PrismaCMS.Application.Common.DTOs
{
    public class FinancialStatementDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }
        public int Year { get; set; }
        public FinancialStatementStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public List<AssignmentDto> Assignments { get; set; } = new();
    }

    public record FinancialStatementCreateDto(
        int CustomerId,
        int Year
    );

    public record FinancialStatementUpdateDto(
        int Year,
        FinancialStatementStatus Status
    );
}