using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Flowspire.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Ocorreu um erro interno no servidor.";

        switch (exception)
        {
            case ArgumentException argEx:
                statusCode = HttpStatusCode.BadRequest;
                message = argEx.Message;
                break;
            case UnauthorizedAccessException authEx:
                statusCode = HttpStatusCode.Forbidden;
                message = authEx.Message;
                break;
            case KeyNotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundEx.Message;
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        var result = JsonConvert.SerializeObject(new { error = message });
        return context.Response.WriteAsync(result);
    }
}