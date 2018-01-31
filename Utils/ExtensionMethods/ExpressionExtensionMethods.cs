using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Utils.Extensions
{
    public static class ExpressionFactory
    {
        public static Expression<Func<TModel, T>> ModelProperty<TModel, T>(string propertyName)
        {
            var propertyInfo = typeof(TModel).GetProperty(propertyName);

            var entityParam = Expression.Parameter(typeof(TModel), "e");
            Expression columnExpr = Expression.Property(entityParam, propertyInfo);

            if (propertyInfo.PropertyType != typeof(T))
                columnExpr = Expression.Convert(columnExpr, typeof(T));

            return Expression.Lambda<Func<TModel, T>>(columnExpr, entityParam);
        }

        public static TProperty PropertyValue<TModel, TProperty>(this TModel model, string propName)
            where TModel: class
        {
            return ModelProperty<TModel, TProperty>(propName).Compile().Invoke(model);
        }

        public static Expression<Func<TInput, dynamic>> Convert<TInput, TOutput>(this Expression<Func<TInput, TOutput>> expression)
        {
            // Add the boxing operation, but get a weakly typed expression
            Expression converted = Expression.Convert(expression.Body, typeof(object));
            // Use Expression.Lambda to get back to strong typing
            return Expression.Lambda<Func<TInput, dynamic>>(converted, expression.Parameters);
        }
    }
}
