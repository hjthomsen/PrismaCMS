using AutoMapper;
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
            // Individual mappings are now in separate profile files:
            // - CustomerMappingProfile
            // - ContactInfoMappingProfile
            // AutoMapper will automatically discover and use all Profile classes
        }
    }
}