using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;

namespace Utils.ExtensionMethods
{
    /// <summary>
    /// ADO Extension Methods 
    /// </summary>
    public static class AdoExtensionMethods
    {
        /// <summary>
        /// Convert DataReader to List<dynamic> 
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<dynamic> ToDynamicList(this DbDataReader reader)
        {
            var result = new List<dynamic>();
            if(reader != null && reader.HasRows)
            {
                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                foreach (IDataRecord record in reader as IEnumerable)
                {
                    var expando = new ExpandoObject() as IDictionary<string, object>;
                    foreach (var name in names)
                        expando[name] = record[name];

                    result.Add(expando);
                }
            }

            return result;
        }
    }
}
