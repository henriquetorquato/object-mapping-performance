using ObjectMappingPerformance.Objects;
using System;
using System.Collections.Generic;

namespace ObjectMappingPerformance.Mappings
{
    public class AttributionMapping : IObjectMapper
    {
        public string Name => "Attribution";

        public IMappingObject Map<T>(IDictionary<string, string> dictionary) where T : IMappingObject
        {
            if (typeof(T) == typeof(SimpleStringObject))
            {
                return new SimpleStringObject
                {
                    Property1 = GetValue<string>(nameof(SimpleStringObject.Property1), dictionary),
                    Property2 = GetValue<string>(nameof(SimpleStringObject.Property2), dictionary),
                    Property3 = GetValue<string>(nameof(SimpleStringObject.Property3), dictionary),
                    Property4 = GetValue<string>(nameof(SimpleStringObject.Property4), dictionary),
                    Property5 = GetValue<string>(nameof(SimpleStringObject.Property5), dictionary)
                };
            }
            else if (typeof(T) == typeof(ValueConversionObject))
            {
                return new ValueConversionObject
                {
                    BoolProperty1 = GetValue<bool>(nameof(ValueConversionObject.BoolProperty1), dictionary),
                    BoolProperty2 = GetValue<bool>(nameof(ValueConversionObject.BoolProperty2), dictionary),
                    IntProperty1 = GetValue<int>(nameof(ValueConversionObject.IntProperty1), dictionary),
                    IntProperty2 = GetValue<int>(nameof(ValueConversionObject.IntProperty2), dictionary),
                    StringProperty1 = GetValue<string>(nameof(ValueConversionObject.StringProperty1), dictionary)
                };
            }
            else
            {
                throw new Exception("Not implemented object");
            }
        }

        private T GetValue<T>(string key, IDictionary<string, string> dictionary)
        {
            try
            {
                dictionary.TryGetValue(key, out var rawValue);
                return (T)Convert.ChangeType(rawValue, typeof(T));
            }
            catch
            {
                return default;
            }
        }
    }
}
