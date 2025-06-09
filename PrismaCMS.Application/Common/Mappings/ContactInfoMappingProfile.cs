using AutoMapper;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Domain.ValueObjects;

namespace PrismaCMS.Application.Common.Mappings
{
    public class ContactInfoMappingProfile : Profile
    {
        public ContactInfoMappingProfile()
        {
            CreateMap<ContactInfoDto, ContactInfo>()
                .ConstructUsing(src => new ContactInfo(
                    src.Email,
                    src.Phone,
                    src.Address,
                    src.City,
                    src.PostalCode,
                    src.Country));

            CreateMap<ContactInfo, ContactInfoDto>();
        }
    }
}