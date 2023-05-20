using GlobalHandlerException.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GlobalHandlerException.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            // Global exception
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error." 
                        }.ToString());
                    }
                });
            });

            
        }

        public static void ConfigureCustomExceptionHandler(this WebApplication app)
        {
            // Custom Global Exception
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
