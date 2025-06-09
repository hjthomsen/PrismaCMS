using AutoMapper;
using PrismaCMS.Application.Common.DTOs;
using PrismaCMS.Domain.Entities;

namespace PrismaCMS.Application.Common.Mappings
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>()
                .ConstructUsing(src => new Employee(src.Name, src.Email, src.Role));
        }
    }
}