using AutoMapper;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Application.Common.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));
        }
    }
}