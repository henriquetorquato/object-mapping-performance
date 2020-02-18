using AutoMapper;
using ObjectMappingPerformance.Objects;
using System.Collections.Generic;

namespace ObjectMappingPerformance.Mappings
{
    public class AutoMapperMapping : IObjectMapper
    {
        public string Name => "AutoMapper";

        private readonly IMapper _mapper;

        public AutoMapperMapping()
        {
            _mapper = new MapperConfiguration(c =>  { })
                .CreateMapper();
        }

        public IMappingObject Map<T>(IDictionary<string, string> dictionary) where T : IMappingObject
            => _mapper.Map<IDictionary<string, object>, T>(dictionary as IDictionary<string, object>);
    }
}
