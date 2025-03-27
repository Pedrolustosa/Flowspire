using Flowspire.API.Middleware;
using Flowspire.Infra.IoC;
using Flowspire.Domain.Hubs;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();
app.UseCors("AllowSpecificOrigins");
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub").RequireAuthorization();
app.Run();