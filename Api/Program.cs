using Api.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
                .ConfigureAppConfig()
                .ConfigureLogging()
                .UseStartup<Startup>()
                .Build();
    }
}
