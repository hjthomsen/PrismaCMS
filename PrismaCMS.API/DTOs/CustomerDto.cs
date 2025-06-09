namespace PrismaCMS.API.DTOs
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
        string? Email,
        string? Phone,
        string? Address,
        string? City,
        string? PostalCode,
        string? Country
    );
}