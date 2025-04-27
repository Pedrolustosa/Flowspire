using Serilog;
using Flowspire.Infra.IoC;
using Flowspire.Domain.Hubs;
using Flowspire.API.Middleware;
using Hangfire;
using Flowspire.Application.Interfaces;
using Flowspire.API.Filters;
using Hangfire.Storage.SQLite;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=flowspire.db";
builder.Services.AddInfrastructure(connectionString, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins(
                  "http://localhost:4200",
                  "https://localhost:4200"
              );
    });
});

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseSQLiteStorage(connectionString);
});

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseAuditLogging();
app.UseCors("AllowSpecificOrigins");
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireJwtAuthorizationFilter() }
});

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub").RequireAuthorization();

RecurringJob.AddOrUpdate<IAuditLogService>(
    "cleanup-old-audit-logs",
    service => service.CleanupOldLogsAsync(),
    Cron.Daily(3)
);

app.Run();
