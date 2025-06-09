using AutoMapper;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Application.Common.Mappings
{
    public class FinancialStatementMappingProfile : Profile
    {
        public FinancialStatementMappingProfile()
        {
            CreateMap<FinancialStatement, FinancialStatementDto>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.Assignments, opt => opt.MapFrom(src => src.Assignments));
        }
    }
}