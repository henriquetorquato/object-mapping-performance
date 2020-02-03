using ObjectMappingPerformance.Objects;
using System;
using System.Collections.Generic;

namespace ObjectMappingPerformance.Mappings
{
    class ReflectionMapping : IObjectMapper
    {
        public string Name => nameof(ReflectionMapping);

        public T Map<T>(IDictionary<string, string> dictionary) where T : IMappingObject
        {
            var type = typeof(T);
            var instance = (T)Activator.CreateInstance(type);

            foreach (var keyValuePair in dictionary)
            {
                var property = type.GetProperty(keyValuePair.Key);
                if (property != null)
                {
                    var value = Convert.ChangeType(keyValuePair.Value, property.PropertyType);
                    property.SetValue(instance, value);
                }
            }

            return instance;
        }
    }
}
