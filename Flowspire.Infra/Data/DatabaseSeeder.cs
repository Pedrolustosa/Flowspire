using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;

namespace Flowspire.Infra.Data;
public static class DatabaseSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        // Usuários
        var adminId = Guid.NewGuid().ToString();
        var advisorId = Guid.NewGuid().ToString();
        var customer1Id = Guid.NewGuid().ToString();
        var customer2Id = Guid.NewGuid().ToString();

        var hasher = new PasswordHasher<User>();
        builder.Entity<User>().HasData(
            new User(adminId, "admin@flowspire.com", "Admin User", hasher.HashPassword(null, "Admin123"))
            {
                NormalizedEmail = "ADMIN@FLOWSPIRE.COM",
                NormalizedUserName = "ADMIN@FLOWSPIRE.COM"
            },
            new User(advisorId, "advisor@flowspire.com", "Financial Advisor", hasher.HashPassword(null, "Advisor123"))
            {
                NormalizedEmail = "ADVISOR@FLOWSPIRE.COM",
                NormalizedUserName = "ADVISOR@FLOWSPIRE.COM"
            },
            new User(customer1Id, "customer1@flowspire.com", "Customer One", hasher.HashPassword(null, "Customer123"))
            {
                NormalizedEmail = "CUSTOMER1@FLOWSPIRE.COM",
                NormalizedUserName = "CUSTOMER1@FLOWSPIRE.COM"
            },
            new User(customer2Id, "customer2@flowspire.com", "Customer Two", hasher.HashPassword(null, "Customer123"))
            {
                NormalizedEmail = "CUSTOMER2@FLOWSPIRE.COM",
                NormalizedUserName = "CUSTOMER2@FLOWSPIRE.COM"
            }
        );

        // Roles
        var adminRoleId = Guid.NewGuid().ToString();
        var advisorRoleId = Guid.NewGuid().ToString();
        var customerRoleId = Guid.NewGuid().ToString();

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = advisorRoleId, Name = "FinancialAdvisor", NormalizedName = "FINANCIALADVISOR" },
            new IdentityRole { Id = customerRoleId, Name = "Customer", NormalizedName = "CUSTOMER" }
        );

        // UserRoles (usando os IDs corretos dos roles)
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminId, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = advisorId, RoleId = advisorRoleId },
            new IdentityUserRole<string> { UserId = customer1Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customer2Id, RoleId = customerRoleId }
        );

        // Categorias
        builder.Entity<Category>().HasData(
            new Category(1, "Alimentação", customer1Id),
            new Category(2, "Transporte", customer1Id),
            new Category(3, "Lazer", customer2Id),
            new Category(4, "Educação", customer2Id)
        );

        // Transações
        builder.Entity<Transaction>().HasData(
            new Transaction(1, "Supermercado", -50.00m, DateTime.UtcNow.AddDays(-5), 1, customer1Id),
            new Transaction(2, "Uber", -20.00m, DateTime.UtcNow.AddDays(-3), 2, customer1Id),
            new Transaction(3, "Cinema", -30.00m, DateTime.UtcNow.AddDays(-2), 3, customer2Id),
            new Transaction(4, "Salário", 1000.00m, DateTime.UtcNow.AddDays(-1), 4, customer2Id)
        );

        // Orçamentos
        builder.Entity<Budget>().HasData(
            new Budget(1, 1, 200.00m, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(30), customer1Id),
            new Budget(2, 3, 100.00m, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(30), customer2Id)
        );

        // Mensagens
        builder.Entity<Message>().HasData(
            new Message(1, customer1Id, advisorId, "Olá, preciso de ajuda com meu orçamento!", DateTime.UtcNow.AddHours(-2), false),
            new Message(2, advisorId, customer1Id, "Claro, vamos analisar suas despesas.", DateTime.UtcNow.AddHours(-1), true),
            new Message(3, customer2Id, advisorId, "Qual é o melhor investimento agora?", DateTime.UtcNow.AddHours(-3), false)
        );

        // Associações Advisor-Customer
        builder.Entity<AdvisorCustomer>().HasData(
            new AdvisorCustomer(advisorId, customer1Id, DateTime.UtcNow.AddDays(-10)),
            new AdvisorCustomer(advisorId, customer2Id, DateTime.UtcNow.AddDays(-5))
        );
    }
}