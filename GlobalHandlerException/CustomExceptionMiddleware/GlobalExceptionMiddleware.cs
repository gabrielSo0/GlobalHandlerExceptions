using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace GlobalHandlerException.CustomExceptionMiddleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Ocorreu um erro durante a execução",
                    Detail = ex.Message,
                    Instance = httpContext.Request.HttpContext.Request.Path
                };

                if(ex is BadHttpRequestException)
                {
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Title = "Requisição inválida";
                    problemDetails.Detail = "Ocorreu um erro na requisição";
                }

                await HandleExceptionAsync(httpContext, problemDetails);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ProblemDetails problemDetail)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)problemDetail.Status;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetail));
        }
    }
}
