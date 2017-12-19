using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Utils.ExtensionMethods
{
    /// <summary>
    /// CommonExtensionMethods 
    /// </summary>
    public static class CommonExtensionMethods
    {
        /// <summary>
        /// Serialize objects using JSON from NewtonSoft
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Concatenates items from IEnumerable with separator and format
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> value, string separator = ",", string formatter = "{0}")
        {
            return value
                .Select(x => String.Format(formatter, x))
                .Aggregate((i, j) => i + separator + j);
        }

        /// <summary>
        /// Has Value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasValue(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// verifies if is null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToNullString(this object value)
        {
            return value != null ? value.ToString() : string.Empty;
        }

        /// <summary>
        /// First letter to invariant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decapitalize(this string value)
        {
            if (value.HasValue())
            {
                return Char.ToLowerInvariant(value[0]) + value.Substring(1);
            }
            return value;
        }

        public static T Cast<T> (this string value)
            where T : IConvertible
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        
    }

    public class Enum<T> where T : struct, IConvertible
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<string> Names
        {
            get
            {
                if (!typeof(T).GetTypeInfo().IsEnum)
                {
                    throw new ArgumentException("T must be an enumerated type");
                }

                return Enum.GetNames(typeof(T)).ToList();
            }
        }
    }
}
