using Newtonsoft.Json;
using ObjectMappingPerformance.Objects;
using System.Collections.Generic;

namespace ObjectMappingPerformance.Mappings
{
    public class SerializationMapping : IObjectMapper
    {
        public string Name => "Serialization";

        public IMappingObject Map<T>(IDictionary<string, string> dictionary) where T : IMappingObject
            => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dictionary));
    }
}
