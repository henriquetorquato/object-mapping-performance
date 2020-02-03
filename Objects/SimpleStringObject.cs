using System.Collections.Generic;

namespace ObjectMappingPerformance.Objects
{
    public class SimpleStringObject : IMappingObject
    {
        public string Name => nameof(SimpleStringObject);

        public IDictionary<string, string> Dictionary
            => new Dictionary<string, string>()
            {
                { "Property1", "Value1" },
                { "Property2", "Value2" },
                { "Property3", "Value3" },
                { "Property4", "Value4" },
                { "Property5", "Value5" }
            };

        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
        public string Property5 { get; set; }
    }
}
