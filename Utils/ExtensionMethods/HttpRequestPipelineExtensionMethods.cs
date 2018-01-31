using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;

namespace Utils.Extensions.HttpRequestPipeline
{
    /// <summary>
    /// Use Service extensions
    /// </summary>
    public static class HttpRequestPipelineExtensionMethods
    {
        /// <summary>
        /// configure logging middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        public static void UseSerilogMiddleware(this IApplicationBuilder app, ILoggerFactory loggerFactory)
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
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if(exceptionHandlerFeature != null)
                        {
                            var error = exceptionHandlerFeature.Error;
                        }

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
