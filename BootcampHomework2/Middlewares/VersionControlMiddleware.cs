using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampHomework2.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class VersionControlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _conf;

        public VersionControlMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _conf = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var currentVersion = new Version(_conf.GetValue<string>("app-version")); // get current version from appsettings.json
            var versionFromRequest = new Version(httpContext.Request.Headers["app-version"]); // get app-version from request header
            try
            {
                if (httpContext.Request.Path == "/register" || httpContext.Request.Path == "/login")
                {
                    await _next(httpContext);
                }
                else if(currentVersion.CompareTo(versionFromRequest) < 0)
                {
                    await UnauthorizedExceptionAsync(httpContext,new Exception("Unauthorized Operation Founded!"));
                }
            }
            catch (Exception ex)
            {
                await ServerErrorExceptionAsync(httpContext, ex);
            }

        }

        private async Task UnauthorizedExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            httpcontext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpcontext.Response.WriteAsync(ex.Message);
        }
        private async Task ServerErrorExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpcontext.Response.WriteAsync($"Internal Server Error Sorry About That! Detail: {ex.Message}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class VersionControlMiddlewareExtensions
    {
        public static IApplicationBuilder UseVersionControlMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VersionControlMiddleware>();
        }
    }
}
