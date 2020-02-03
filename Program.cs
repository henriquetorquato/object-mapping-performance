using ObjectMappingPerformance.Mappings;
using ObjectMappingPerformance.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ObjectMappingPerformance
{
    class Program
    {
        private const int Iterations = 100000;

        static void Main(string[] args)
        {
            var mappers = new List<IObjectMapper>()
            {
                new ReflectionMapping(),
                new SerializationMapping(),
                new ExpressionTreeMapping()
            };

            foreach (var mapper in mappers)
            {
                Console.WriteLine($"Mapping performance for {mapper.Name} on {Iterations} iterations:");

                GenerateMetricsFor<SimpleStringObject>(mapper);
                GenerateMetricsFor<ValueConversionObject>(mapper);

                Console.WriteLine();
            }
        }

        static void GenerateMetricsFor<T>(IObjectMapper objectMapping) where T : IMappingObject
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            var time = GetMappingTime<T>(objectMapping, Iterations, instance.Dictionary);

            Console.WriteLine($"\t{instance.Name} took {time.TotalMilliseconds}ms");
        }

        static TimeSpan GetMappingTime<T>(IObjectMapper objectMapping, int iterations, IDictionary<string, string> dictionary) where T : IMappingObject
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                objectMapping.Map<T>(dictionary);
            }

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
    }
}
