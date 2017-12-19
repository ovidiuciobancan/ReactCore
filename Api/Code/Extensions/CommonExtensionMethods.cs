using System;
using System.Linq;
using System.Reflection;
using Utils;
using Utils.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class CommonExtensionMethods
    {
        /// <summary>
        /// Gets Typed App Settings from appsettings.json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static T GetSettings<T>(this IConfiguration config, string sectionName = "AppSettings")
            where T : class, new()
        {
            var appSettings = new T();
            var appSettingsSection = config.GetSection(sectionName);
            typeof(T).GetTypeInfo().GetProperties().ToList().ForEach(p =>
            {
                var configValue = appSettingsSection.GetValue(p.PropertyType, p.Name);
                if (p.SetMethod != null && configValue != null)
                {
                    p.SetValue(appSettings, configValue);
                }
            });

            return appSettings;
        }

        /// <summary>
        /// Inject all the derived types based on a given type scoped
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static IServiceCollection AddDerivedFrom<T>(this IServiceCollection service, Func<Type, IServiceCollection> scope)
        {
            typeof(T)
                .GetDerivedTypes(typeof(Startup).GetTypeInfo().Assembly)
                .Where(t => !t.HasAttribute<IgnoreInjectionAttribute>() &&
                            t.GetTypeInfo().IsAbstract == false)
                .ToList()
                .ForEach(t => service = scope(t));

            return service;
        }
    }
}
