using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Extensions
{
    public static class ConfigurationExtensionMethods
    {
        /// <summary>
        /// Generic Gets Typed App Settings from appsettings.json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        public static T GetConfiguration<T>(this IConfiguration config, string sectionName = null)
            where T : class, new()
        {
            var result = new T();
            sectionName = sectionName ?? typeof(T).GetTypeInfo().Name; 
            config.GetSection(sectionName).Bind(result);
            return result;
        }

        /// <summary>
        /// Gets Typed App Settings from appsettings.json
        /// </summary>
        /// <param name="returnType"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static object GetSettings(this IConfigurationSection configSection, Type returnType)
        {
            var appSectionSettings = Activator.CreateInstance(returnType);
            returnType.GetTypeInfo().GetProperties().ToList().ForEach(p =>
            {
                if (p.SetMethod != null)
                {
                    var configSubSection = configSection.GetSection(p.Name);
                    var configValue = configSection.GetValue(p.PropertyType, p.Name);
                    if (configValue != null)
                    {
                        p.SetValue(appSectionSettings, configValue);
                        
                    }
                    else if(configSubSection != null)
                    {
                        p.SetValue(appSectionSettings, configSubSection.GetSettings(p.PropertyType));
                    }
                }
            });

            return appSectionSettings;

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
