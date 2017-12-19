using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api.Extensions
{
    /// <summary>
    /// Use Service extensions
    /// </summary>
    public static class HttpRequestPipelineConfigurator
    {
        public static void UseSerilog(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();
        }

        /// <summary>
        /// configure exception middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseExceptionMiddleware(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        //context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later...");
                    });
                });
            }
        }
        /// <summary>
        /// configure webpack dev middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void UseWebpackMiddleware(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
        }
    }
}
