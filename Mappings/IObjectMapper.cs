using ObjectMappingPerformance.Objects;
using System.Collections.Generic;

namespace ObjectMappingPerformance.Mappings
{
    public interface IObjectMapper
    {
        string Name { get; }

        T Map<T>(IDictionary<string, string> dictionary) where T : IMappingObject;
    }
}
