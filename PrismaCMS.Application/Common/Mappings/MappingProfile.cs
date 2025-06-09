using AutoMapper;
using PrismaCMS.Domain.Entities;
using PrismaCMS.Domain.ValueObjects;
using System.Reflection;

namespace PrismaCMS.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            // This method would scan for classes that implement IMapFrom<T> if needed
            // But for now we'll create mappings manually

            // We'll define mappings for Customer and ContactInfo
            CreateMap<ContactInfoDto, ContactInfo>()
                .ConstructUsing(src => new ContactInfo(
                    src.Email,
                    src.Phone,
                    src.Address,
                    src.City,
                    src.PostalCode,
                    src.Country));

            CreateMap<ContactInfo, ContactInfoDto>();

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));
        }
    }

    // DTOs for mapping
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