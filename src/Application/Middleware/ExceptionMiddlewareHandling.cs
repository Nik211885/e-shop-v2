using Application.Models;
using Core.Exceptions;
using FluentValidation;
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
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            var controllerName = context.GetRouteData()?.Values["controller"]?.ToString();
            var actionName = context.GetRouteData()?.Values["action"]?.ToString();
            var requestMethod = context.Request.Method;
            var requestPath = context.Request.Path;
            // change to utc + 7
            var time = DateTimeOffset.UtcNow.AddHours(7);
            var message =
                $"{time} - Exception occurred at {controllerName}/{actionName} - Path: {requestPath}, Method: {requestMethod}";
            var statusCode = GetStatusCode(exception);
            string title;
            string messageDetail;
            // change to utc + 7
            if (statusCode >= 500)
            {
                title = "Error";
                logger.LogError(exception,message);
                messageDetail = "Có lỗi ở server";
            }
            else
            {
                title = "Warning";
                logger.LogWarning(message);
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
                ValidationException=> StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}