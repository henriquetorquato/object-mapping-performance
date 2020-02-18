using ObjectMappingPerformance.Objects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ObjectMappingPerformance.Mappings
{
    public class ExpressionTreeMapping : IObjectMapper
    {
        public string Name => "ExpressionTree";

        private static ConcurrentDictionary<Type, Delegate> _mapperCache = new ConcurrentDictionary<Type, Delegate>();

        public static Func<IDictionary<string, string>, T> GetMapper<T>()
        {
            var targetType = typeof(T);

            Delegate mapper;
            if (_mapperCache.TryGetValue(targetType, out mapper))
            {
                return (Func<IDictionary<string, string>, T>)mapper;
            }

            var configurationsType = typeof(IDictionary<string, string>);
            var statements = new List<Expression>();

            var instance = Expression.Variable(targetType, "instance");
            var configurations = Expression.Parameter(configurationsType, "configurations");

            var configurationsItem = configurationsType.GetProperty("Item", new[] { typeof(string) });
            var changeType = typeof(Convert).GetMethod("ChangeType", new[] { typeof(string), typeof(Type) });

            statements.Add(Expression.Assign(instance, Expression.New(targetType)));

            foreach (var property in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanWrite)
                {
                    continue;
                }

                var getRawValue = Expression.MakeIndex(configurations, configurationsItem,
                    new[] { Expression.Constant(property.Name) });

                var convertValue = Expression.Convert(
                        Expression.Call(changeType, getRawValue, Expression.Constant(property.PropertyType)), property.PropertyType);

                var assignValue = Expression.Assign(Expression.Property(instance, property), convertValue);

                var tryAssignValue = Expression.TryCatch(
                    Expression.Block(
                        assignValue,
                        Expression.Empty()
                    ),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Empty()
                    )
                );

                statements.Add(tryAssignValue);
            }

            statements.Add(instance);

            var body = Expression.Block(targetType, new[] { instance }, statements.ToArray());

            mapper = Expression
                .Lambda<Func<IDictionary<string, string>, T>>(body, configurations)
                .Compile();

            _mapperCache.TryAdd(targetType, mapper);

            return (Func<IDictionary<string, string>, T>)mapper;
        }

        public IMappingObject Map<T>(IDictionary<string, string> dictionary) where T : IMappingObject
        {
            var mapper = GetMapper<T>();
            return mapper(dictionary);
        }
    }
}
