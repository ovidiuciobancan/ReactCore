using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api.Extensions
{
    public static class WebHostConfigurations
    {
        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging
                    .AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                    .AddSerilog()
                    .AddConsole()
                    .AddDebug();
            });
        }

        public static IWebHostBuilder ConfigureAppConfig(this IWebHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((builderContext, config) =>
            {
                IHostingEnvironment env = builderContext.HostingEnvironment;

                config
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            });
        }
            
    }
}
