using System.Net;
using Newtonsoft.Json;
using Flowspire.Application.Exceptions;

namespace Flowspire.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocorreu um erro interno no servidor.";

            switch (exception)
            {
                case ValidationException validationEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = validationEx.Message;
                    break;
                case UnauthorizedException unauthorizedEx:
                    statusCode = HttpStatusCode.Forbidden;
                    message = unauthorizedEx.Message;
                    break;
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundEx.Message;
                    break;
                case ArgumentException argEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = argEx.Message;
                    break;
                case UnauthorizedAccessException authEx:
                    statusCode = HttpStatusCode.Forbidden;
                    message = authEx.Message;
                    break;
                case KeyNotFoundException keyEx:
                    statusCode = HttpStatusCode.NotFound;
                    message = keyEx.Message;
                    break;
                default:
                    _logger.LogError(exception, "Unhandled exception occurred.");
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                timestamp = DateTime.UtcNow,
                status = context.Response.StatusCode,
                error = message,
                path = context.Request.Path,
                traceId = context.TraceIdentifier
            };

            var result = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}