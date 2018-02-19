using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DA.Base;
using Microsoft.EntityFrameworkCore;
using Common.Interfaces;
using Common.Implementation;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;
using BL.Services;
using Microsoft.Extensions.Logging;
using Utils.Extensions;
using Utils.Extensions.HttpRequestPipeline;
using Utils.Extensions.Mapper;
using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using FluentValidation.AspNetCore;
using Api.Validators;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public AppSettings AppSettings { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            AppSettings = configuration.GetConfiguration<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add typed AppSettings from appsettings.json
            services.AddSingleton<IAppSettings>(AppSettings);

            //// Add open id connect with IdentityServer4 authentication
            //services
            //    .AddAuthentication()
            //    .AddIdentityServerAuthentication(config =>
            //    {
            //        config.RequireHttpsMetadata = true;
            //        config.Authority = AppSettings.AuthAuthority;
            //        config.ApiName = "api";
            //    });

            // Add distributed cache
            services.AddDistributedMemoryCache();

            // Add EF Db Context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Add HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //Add MVC
            services
                .AddMvc(config =>
                {
                    config.ReturnHttpNotAcceptable = true;
                    config.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    config.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
                })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CommonValidator>()).Services
                .AddMvcHelpers();

 

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

            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = AppSettings.AuthSettings.Authority;
                    options.RequireHttpsMetadata = true;
                    options.ApiName = "api";
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:44310")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            app.UseExceptionMiddleware(env);

            app.UseSerilogMiddleware(loggerFactory);

            app.UseAuthentication();

            app.UseCors("default");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "apiRoute",
                  template: "api/{controller}/{id?}"
                );
            });
        }
    }
}
