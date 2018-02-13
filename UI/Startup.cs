using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utils.Extensions;
using Utils.Extensions.HttpRequestPipeline;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetConfiguration<AppSettings>();
        }

        public IConfiguration Configuration { get; }
        public AppSettings AppSettings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add typed AppSettings from appsettings.json
            services.AddSingleton<IAppSettings>(AppSettings);

            // Add open id connect with IdentityServer4 authentication
            //services.AddHybridAuth(opt =>
            //{
            //    opt.OpenIdConnectOptions.Authority = AppSettings.AuthAuthority;
            //    opt.OpenIdConnectOptions.ClientId = AppSettings.ClientAppId;
            //    opt.OpenIdConnectOptions.ClientSecret = AppSettings.ClientSecret;
            //    opt.OpenIdConnectOptions.Scope.Add(AppSettings.ApiName);
            //});

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExceptionMiddleware(env);

            app.UseSerilogMiddleware(loggerFactory);

            app.UseAuthentication();

            app.UseWebpackMiddleware(env);

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
