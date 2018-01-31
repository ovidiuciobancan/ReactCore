using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utils.Extensions
{
    public static class EFCoreExtensions
    {
        public static IEnumerable<IProperty> PrimaryKey<T>(this DbContext context)
            where T : class, new()
        {
            return context.Model
                .FindEntityType(typeof(T))
                .FindPrimaryKey()
                .Properties;
        }

        public static T Stub<T>(this IEnumerable<IProperty> properties, params object[] values)
            where T : class, new()
        {
            if (properties == null || values == null || properties.Count() != values.Count())
            {
                throw new ArgumentException("Paramters must be not null and have save length");
            }

            var result = new T();

            properties
                .Select(p => p.Name)
                .Select(p => typeof(T).GetProperty(p))
                .Zip(values, (prop, value) => new
                {
                    Property = prop,
                    Value = value
                })
                .ToList()
                .ForEach(item => item.Property.SetValue(result, item.Value));

            return result;
        }

        public static bool Has(this DbContext context, string name)
        {
            return context.Model.GetEntityTypes().Any(t => t.Name.Contains(name));
        }
        public static bool Has<T>(this DbContext context)
        {
            return context.Has(typeof(T).Name);
        }

        public static T CloneEntity<T>(this T entity)
            where T : class, new()
        {
            var clone = entity.Clone();
            clone.GetProperties()
                .Where(p => !p.PropertyType.GetTypeInfo().IsValueType && p.PropertyType != typeof(string))
                .ToList()
                .ForEach(p => clone.SetProperty(p, default(T)));

            return clone;
        }
    }
}
