using AutoMapper;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Application.Common.Mappings
{
    public class TimeEntryMappingProfile : Profile
    {
        public TimeEntryMappingProfile()
        {
            CreateMap<TimeEntry, TimeEntryDto>()
                .ForMember(dest => dest.Assignment, opt => opt.MapFrom(src => src.Assignment));
        }
    }
}