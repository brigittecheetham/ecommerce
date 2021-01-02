using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, ex.Message);
               var internalServerError = (int)HttpStatusCode.InternalServerError;

               context.Response.ContentType = "application/json";
               context.Response.StatusCode = internalServerError;
               
               var response = _hostEnvironment.IsDevelopment() 
                ? new ApiException(internalServerError, ex.Message, ex.StackTrace.ToString()) 
                : new ApiException(internalServerError);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

    }
}