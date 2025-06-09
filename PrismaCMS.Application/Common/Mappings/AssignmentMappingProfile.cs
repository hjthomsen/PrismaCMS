using AutoMapper;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Application.Common.Mappings
{
    public class AssignmentMappingProfile : Profile
    {
        public AssignmentMappingProfile()
        {
            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.FinancialStatement, opt => opt.MapFrom(src => src.FinancialStatement))
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
                .ForMember(dest => dest.TimeEntries, opt => opt.MapFrom(src => src.TimeEntries));
        }
    }
}