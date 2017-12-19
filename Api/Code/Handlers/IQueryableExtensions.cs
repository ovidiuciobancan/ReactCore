//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace API.Helpers
//{
//    public static class IQueryableExtensions
//    {
//        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string,PropertyMappingValue> mappingDictionary)
//        {
//            if(source == null)
//            {
//                throw new ArgumentException("source");
//            }

//            if(mappingDictionary == null)
//            {
//                throw new ArgumentException("mappingDirectory");
//            }

//            if (string.IsNullOrWhiteSpace(orderBy))
//            {
//                return source;
//            }

//            var orderByAfterSplit = orderBy.Split(',');

//            foreach(var orderByClause in orderByAfterSplit.Reverse())
//            {
//                var trimmedOrderByClause = orderByClause.Trim();

//                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

//                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
//                var propertyName = indexOfFirstSpace == -1 ?
//                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

//                if(!mappingDictionary.ContainsKey(propertyName))
//                {
//                    throw new ArgumentException("Key mapping for propname is missing");
//                }

//                var propertyMappingValue = mappingDictionary[propertyName];

//                if(propertyMappingValue == null)
//                {
//                    throw new ArgumentNullException("propertyMappingValue");
//                }

//                foreach (var destinationProperty in propertyMappingValue.DestionationProperties.Reverse())
//                {
//                    if(propertyMappingValue.Revert)
//                    {
//                        orderDescending = !orderDescending;
//                    }
//                    source = source.OrderBy(destinationProperty + (orderDescending ? " descending" : " ascending"));
//                }

//            }
//            return source;


//        }

//    }
//}
