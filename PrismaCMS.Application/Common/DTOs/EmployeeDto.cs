using PrismaCMS.Domain.Enums;

namespace PrismaCMS.Application.Common.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EmployeeRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }

    public record EmployeeCreateDto(
        string Name,
        string Email,
        EmployeeRole Role
    );

    public record EmployeeUpdateDto(
        string Name,
        string Email,
        EmployeeRole Role
    );
}