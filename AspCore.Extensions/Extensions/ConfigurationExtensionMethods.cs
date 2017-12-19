using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspCore.Extensions.Configuration
{
    public static class ConfigurationExtensionMethods
    {
        public static IServiceCollection AddAppSettings<T>(this IServiceCollection services, IConfiguration config)
           where T : class, new()
        {
            var appSettings = config.GetSettings<T>();
            return services.AddSingleton(appSettings);
        }

        public static T GetSettings<T>(this IConfiguration config)
            where T : class, new()
        {
            var appSettings = new T();
            var appSettingsSection = config.GetSection("AppSettings");
            typeof(T).GetTypeInfo().GetProperties().ToList().ForEach(p =>
            {
                var configValue = appSettingsSection[p.Name];
                if (p.SetMethod != null && configValue != null)
                {
                    p.SetValue(appSettings, configValue);
                }
            });

            return appSettings;
        }
    }
}
