using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace Utils.ExtensionMethods
{
    /// <summary>
    /// Reflection Type Extension Methods
    /// </summary>
    public static class TypeExtensionMethods
    {
        /// <summary>
        /// Verity if type has property
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName) != null;
        }

        /// <summary>
        /// Gets property value by name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetValue<T>(this T obj, string propertyName)
        {
            return obj.GetType().GetTypeInfo().GetProperty(propertyName)?.GetValue(obj);
        }

        public static IEnumerable<PropertyInfo> GetProperties<T>(this T obj)
        {
            return obj.GetType().GetTypeInfo().GetProperties();
        }

        public static void SetProperty<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> predicate, TProperty value)
        {
            var propertyName = (predicate.Body as MemberExpression)?.Member?.Name;
            model.SetProperty(propertyName, value);
        }
        public static void SetProperty<TModel, TProperty>(this TModel model, string propertyName, TProperty value)
        {
            var property = model.GetType().GetTypeInfo().GetProperty(propertyName);
            model.SetProperty(property, value);
            
        }
        public static void SetProperty<TModel, TProperty>(this TModel model, PropertyInfo property, TProperty value)
        {
            if (property?.CanWrite ?? false)
            {
                property.SetValue(model, value);
            }
        }

        public static TModel Clone<TModel>(this TModel model)
            where TModel: class, new()
        {
            var result = new TModel();
            model.GetProperties().ToList()
                .ForEach(p => 
                    result.SetProperty(p.Name, model.GetValue(p.Name))
                );

            return result;
        }


        /// <summary>
        /// Verify if type has attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this Type type)
            where T : Attribute
        {
            return type.GetTypeInfo().CustomAttributes.Any(a => a.AttributeType == type);
        }

        /// <summary>
        /// Get derived types from a base type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetDerivedTypes(this Type type)
        {
            return type.GetDerivedTypes(type.GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Get derived types from a base type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entryAssembly"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetDerivedTypes(this Type type, Assembly entryAssembly)
        {
            return new List<Assembly>() { entryAssembly }
                .Concat(entryAssembly
                    .GetReferencedAssemblies()
                    .Select(a => Assembly.Load(a)))
                .SelectMany(a => a.GetTypes())
                .Where(t => t != type && type.IsAssignableFrom(t));
        }

        public static string GetSimpleName(this Type type)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                return type.GetGenericTypeDefinition().Name;
            }

            return type.Name;
        }

        public static bool HasInterface<T>(this Type type)
        {
            return type
                .GetInterfaces()
                .Any(i => i.
                    Name.Contains(typeof(T).GetSimpleName())
                );
        }
    }
}
