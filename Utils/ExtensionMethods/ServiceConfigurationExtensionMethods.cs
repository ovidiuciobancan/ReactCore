using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Extensions
{
    public static class ServiceConfigurationExtensionMethods
    {
        public static IServiceCollection AddAppSettings<TInterface,TImplementation>(this IServiceCollection services, IConfiguration config)
            where TInterface: class
            where TImplementation : class, TInterface, new()
        {
            var appSettings = config.GetSettings<TImplementation>();
            return services.AddSingleton<TInterface, TImplementation>(serviceProvider => appSettings);
        }

        public static IServiceCollection AddAppSettings<T>(this IServiceCollection services, IConfiguration config)
            where T : class, new()
        {
            var appSettings = config.GetSettings<T>();
            return services.AddSingleton(appSettings);
        }
    }
}
