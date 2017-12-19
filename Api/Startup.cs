using Api.Extensions;
using Common;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AspCore.Extensions.Auth;
using DA.Base;
using Microsoft.EntityFrameworkCore;
using Common.Interfaces;
using Common.Implementation;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;
using BL.Services;
using Api.Extensions.Mapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public AppSettings AppSettings { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            AppSettings = configuration.GetSettings<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add typed AppSettings from appsettings.json
            services.AddAppSettings<AppSettings>(Configuration);

            // Add open id connect with IdentityServer4 authentication
            services.AddHybridAuth(opt =>
            {
                opt.OpenIdConnectOptions.Authority = AppSettings.AuthAuthority;
                opt.OpenIdConnectOptions.ClientId = AppSettings.ClientAppId;
                opt.OpenIdConnectOptions.ClientSecret = AppSettings.ClientSecret;
                opt.OpenIdConnectOptions.Scope.Add("apiclient");

                opt.IdentityServerAuthenticationOptions.Authority = AppSettings.AuthAuthority;
                opt.IdentityServerAuthenticationOptions.ApiName = "apiclient";
            });

            // Add distributed cache
            services.AddDistributedMemoryCache();

            // Add EF Db Context
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add MVC
            services
                .AddMvc(config => {
                    config.ReturnHttpNotAcceptable = true;
                    config.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                })
                .AddFluentValidation();

            

            //Add HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add Cache
            services.AddSingleton<ICacheProvider, MemoryCacheProvider>();

            // Add Unit of work
            services.AddDerivedFrom<IUnitOfWork>(services.AddScoped);

            // Add Api Services
            services.AddDerivedFrom<IService>(services.AddScoped);

            //Add Cache Service
            services.AddTransient<ICacheService, AppCacheService>();

            //Add Mappers
            services.AddMapper();

            //.AddCurrentUser()
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExceptionMiddleware(env);

            app.UseSerilog(loggerFactory);

            app.UseAuthentication();

            app.UseWebpackMiddleware(env);
            
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "apiRoute",
                    template: "api/{controller}/{id?}");
                routes.MapSpaFallbackRoute(
                            name: "spa-fallback",
                            defaults: new { controller = "Authors" });
            });
        }
    }
}
