using System.Collections.Generic;

namespace ObjectMappingPerformance.Objects
{
    public class ValueConversionObject : IMappingObject
    {
        public string Name => nameof(ValueConversionObject);

        public IDictionary<string, string> Dictionary
            => new Dictionary<string, string>()
            {
                { "BoolProperty1", "True" },
                { "BoolProperty2", "False" },
                { "IntProperty1", "15" },
                { "IntProperty2", "19000" },
                { "StringProperty1", "Value1" }
            };

        public bool BoolProperty1 { get; set; }
        public bool BoolProperty2 { get; set; }
        public int IntProperty1 { get; set; }
        public int IntProperty2 { get; set; }
        public string StringProperty1 { get; set; }
    }
}
