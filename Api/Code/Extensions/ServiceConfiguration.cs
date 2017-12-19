using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddAppSettings<T>(this IServiceCollection services, IConfiguration config)
            where T : class, new()
        {
            var appSettings = config.GetSettings<T>();
            return services.AddSingleton(appSettings);
        }
    }
}
