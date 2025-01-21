using Application.Models;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Middleware
{
    public class ExceptionMiddlewareHandling(ILogger<ExceptionMiddlewareHandling> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var controllerName = context.GetRouteData()?.Values["controller"]?.ToString();
            var actionName = context.GetRouteData()?.Values["action"]?.ToString();
            var requestMethod = context.Request.Method;
            var requestPath = context.Request.Path;
            // change to utc + 7
            var time = DateTimeOffset.UtcNow.AddHours(7);
            var title = "Result: {0}";
            var message =
                $"{title}: {time} - Exception occurred at {controllerName}/{actionName} - Path: {requestPath}, Method: {requestMethod}";
            try
            {
                await next(context);
                logger.LogInformation(string.Format(message, "Success"));
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex, message);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext context, Exception exception, string message)
        {
            var statusCode = GetStatusCode(exception);
            string title;
            string messageDetail;
            // change to utc + 7
            if (statusCode >= 500)
            {
                title = "Error";
                logger.LogError(exception,string.Format(message, title));
                messageDetail = "Có lỗi ở server";
            }
            else
            {
                title = "Warning";
                logger.LogWarning(exception,string.Format(message, title));
                messageDetail = exception.Message;
            }

            var response = new ErrorResponse(
                title, 
                statusCode, 
                messageDetail);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}