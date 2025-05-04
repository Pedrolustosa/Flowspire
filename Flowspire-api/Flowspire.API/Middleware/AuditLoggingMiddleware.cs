using System.Diagnostics;
using System.Security.Claims;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Flowspire.API.Middleware;

public class AuditLoggingMiddleware(
    RequestDelegate next,
    ILogger<AuditLoggingMiddleware> logger,
    IServiceProvider serviceProvider)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<AuditLoggingMiddleware> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            var userId = context.User.Identity?.IsAuthenticated == true
                ? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                : null;

            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == "::1") ipAddress = "127.0.0.1";

            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();

                    // Cria o log usando o método de fábrica
                    var auditLog = AuditLog.Create(
                        userId,
                        context.Request.Method,
                        context.Request.Path,
                        context.Response.StatusCode,
                        elapsedMilliseconds,
                        ipAddress);

                    await auditLogRepository.AddAsync(auditLog);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to save audit log to the database.");
                }
            }

            _logger.LogInformation(
                "AUDIT => UserId: {UserId}, IP: {IP}, Method: {Method}, Path: {Path}, StatusCode: {StatusCode}, ExecutionTimeMs: {ElapsedMilliseconds}",
                userId,
                ipAddress,
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsedMilliseconds
            );
        }
    }
}
