using ObjectMappingPerformance.Mappings;
using ObjectMappingPerformance.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ObjectMappingPerformance
{
    class Program
    {
        private const int COLUMN_SIZE = 15;

        private static List<IObjectMapper> _mappers = new List<IObjectMapper>
        {
            new AttributionMapping(),
            new ReflectionMapping(),
            new SerializationMapping(),
            new ExpressionTreeMapping(),
            new AutoMapperMapping()
        };

        private static List<int> _iterations = new List<int>
        {
            10000,
            30000,
            50000,
            70000,
            90000
        };

        static void Main(string[] args)
        {
            CreateTableFor<SimpleStringObject>();
            CreateTableFor<ValueConversionObject>();
        }

        private static void AddColumn(string content)
            => Console.Write(content.PadRight(COLUMN_SIZE));

        private static void AddRow(IEnumerable<string> columns)
        {
            Console.Write("\t|");
            foreach (var column in columns)
            {
                AddColumn(column);
                Console.Write("|");
            }
            Console.WriteLine();
        }

        private static void AddHeaderSplit()
            => AddRow(
                Enumerable.Repeat(
                    String.Join(string.Empty, Enumerable.Repeat("-", COLUMN_SIZE)),
                _iterations.Count + 1)
            );

        static void CreateTableFor<T>() where T : IMappingObject
        {
            var type = typeof(T);
            Console.WriteLine($"Mapping performance for {type.Name}\n\t");

            _iterations.Sort();
            AddRow(_iterations.Select(i => i.ToString()).Prepend("Iterations"));
            AddHeaderSplit();

            foreach (var mapper in _mappers)
            {
                var mapperTimes = new List<TimeSpan>();
                foreach (var iteration in _iterations)
                {
                    mapperTimes.Add(GetIterationTimeFor<T>(mapper, iteration));
                }

                AddRow(mapperTimes.Select(t => $"{t.Milliseconds}ms").Prepend(mapper.Name));
            }

            Console.WriteLine();
        }

        static TimeSpan GetIterationTimeFor<T>(IObjectMapper objectMapping, int iterations) where T : IMappingObject
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            return GetMappingTime<T>(objectMapping, iterations, instance.Dictionary);
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
