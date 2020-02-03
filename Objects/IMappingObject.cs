using System.Collections.Generic;

namespace ObjectMappingPerformance.Objects
{
    public interface IMappingObject
    {
        string Name { get; }

        IDictionary<string, string> Dictionary { get; }
    }
}
