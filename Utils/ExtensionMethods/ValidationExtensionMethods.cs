using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Utils.ExtensionMethods;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Utils.ExtensionMethods
{
    public static class ValidationExtensionMethods
    {
        /// <summary>
        /// Maps the error from an invalid ModelState to a Dictionary
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(p => p.Value.Errors.Any())
                .ToDictionary
                (
                    p => p.Key.Decapitalize(),
                    p => p.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .Aggregate((a, e) => a + "," + e)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetModelKey<F, T>(Expression<Func<F, T>> expression)
        {
            return ExpressionHelper.GetExpressionText(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="modelState"></param>
        /// <param name="message"></param>
        public static void AddModelError<TModel>(this ModelStateDictionary modelState, string message)
        {
            modelState.AddModelError<TModel, dynamic>(message);
        }

        /// <summary>
        /// Adds error to model state
        /// </summary>
        /// <typeparam name="TModel"></typeparam>/
        /// <typeparam name="TProperty"></typeparam>/
        /// <param name="modelState"></param>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public static void AddModelError<TModel, TProperty>(this ModelStateDictionary modelState, string message, Expression<Func<TModel, TProperty>> expression = null)
        {
            var property = expression != null ? GetModelKey(expression) : "_error";
            modelState.TryAddModelError(property, message);
        }
    }
}
