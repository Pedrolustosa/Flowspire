using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Flowspire.Infra.Data;
using Flowspire.Infra.Repositories;
using Flowspire.Application.Interfaces;
using Flowspire.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.Entities;
using Microsoft.Extensions.Configuration; // Para IdentityRole

namespace Flowspire.Infra.IoC;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, IConfiguration configuration)
    {
        // Configuração do DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        // Configuração do Identity com Roles
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddRoles<IdentityRole>(); // Adiciona suporte a Roles

        // Injeção de dependências
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITransactionService, TransactionService>();

        // Configuração de autenticação JWT
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

        // Adicionar configuração do Swagger
        services.AddSwaggerConfiguration();

        return services;
    }
}