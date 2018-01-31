using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToQuerystring;
using Utils.Extensions;

namespace Utils.ExtensionMethods.OData
{
    public static class ODataExtensionMethods
    {
        public static IQueryable<T> ParseODataString<T>(this string odataQueryString)
        {
            var query = new List<T>().AsQueryable();
            try
            {
                return query.LinqToQuerystring(odataQueryString);
            }
            catch
            {
                return query;
            }
        }


        public static Expression<Func<T, bool>> ParseODataFilterString<T>(this string odataQueryString)
        {
            IQueryable<T> query = null;
            try
            {
                query = new List<T>().AsQueryable().LinqToQuerystring("?$filter=" + odataQueryString);
            }
            catch
            {
                return (t) => true;
            }
           

            var methodCall = query.Expression as MethodCallExpression;
            if (query == null
                || methodCall.Method.Name != "Where"
                || methodCall.Method.DeclaringType != typeof(Queryable))
                throw new Exception("cannot process filter query");

            var predicate = methodCall.Arguments[1];
            var unary = predicate as UnaryExpression;
            if (unary != null)
                predicate = unary.Operand;

            return predicate as Expression<Func<T, bool>>;

        }

        public static Expression<Func<T, U>> ParseODataOrderByString<T, U>(this string odataQueryString)
        {
            IQueryable<T> query = null;
            try
            {
                query = new List<T>().AsQueryable().LinqToQuerystring("?$orderby=" + odataQueryString);
            }
            catch(Exception e)
            {
                return t => default(U);
            }

            var methodCall = query.Expression as MethodCallExpression;
            if (query == null
                || !new List<string>() { "OrderBy" , "OrderByDescending" }.Contains(methodCall.Method.Name)
                || methodCall.Method.DeclaringType != typeof(Queryable))
                throw new Exception("cannot process sort query");

            var predicate = methodCall.Arguments[1];
            var unary = predicate as UnaryExpression;
            if (unary != null)
                predicate = unary.Operand;

            return predicate as Expression<Func<T, U>>;
        }
    }
}
