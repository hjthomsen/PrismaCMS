namespace PrismaCMS.Application.Common.DTOs
{
    public record CustomerCreateDto(
        string Name,
        string OrgNumber,
        string? Email,
        string? Phone,
        string? Address,
        string? City,
        string? PostalCode,
        string? Country
    );

    public record CustomerUpdateDto(
        string? Name,
        ContactInfoDto? ContactInfo
    );

    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string OrgNumber { get; set; } = string.Empty;
        public ContactInfoDto ContactInfo { get; set; } = new ContactInfoDto();
    }

    public class ContactInfoDto
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}