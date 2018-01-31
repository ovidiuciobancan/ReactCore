using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Extensions
{
    public static class ConfigurationExtensionMethods
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

        public static IServiceCollection AddDerivedFrom<T>(this IServiceCollection services, Func<Type, IServiceCollection> scope, Assembly assembly = null)
        {
            typeof(T)
                .GetDerivedTypes(assembly ?? Assembly.GetCallingAssembly())
                .Where(t => !t.HasAttribute<IgnoreInjectionAttribute>() &&
                            t.GetTypeInfo().IsAbstract == false)
                .ToList()
                .ForEach(t => services = scope(t));

            return services;
        }

        public static IServiceCollection AddMvcHelpers(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper, UrlHelper>(serviceProvider => 
            {
                var actionContext = serviceProvider.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            return services;
        }
    }
}
