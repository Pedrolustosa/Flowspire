using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Infra.Data;

public static class DatabaseSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        try
        {
            var hasher = new PasswordHasher<User>();
            SeedUsersAndRoles(builder, hasher);
            SeedCategories(builder);
            SeedTransactions(builder);
            SeedBudgets(builder);
            SeedMessages(builder);
            SeedAdvisorCustomer(builder);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao aplicar seed no banco de dados.", ex);
        }
    }

    private static void SeedUsersAndRoles(ModelBuilder builder, PasswordHasher<User> hasher)
    {
        var adminId = "550e8400-e29b-41d4-a716-446655440001";
        var advisor1Id = "550e8400-e29b-41d4-a716-446655440002";
        var advisor2Id = "550e8400-e29b-41d4-a716-446655440003";
        var customer1Id = "550e8400-e29b-41d4-a716-446655440004";
        var customer2Id = "550e8400-e29b-41d4-a716-446655440005";
        var customer3Id = "550e8400-e29b-41d4-a716-446655440006";

        builder.Entity<User>().HasData(
            new
            {
                Id = adminId,
                UserName = "admin@flowspire.com",
                NormalizedUserName = "ADMIN@FLOWSPIRE.COM",
                Email = "admin@flowspire.com",
                NormalizedEmail = "ADMIN@FLOWSPIRE.COM",
                FullName = "Admin User",
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true
            },
            new
            {
                Id = advisor1Id,
                UserName = "advisor1@flowspire.com",
                NormalizedUserName = "ADVISOR1@FLOWSPIRE.COM",
                Email = "advisor1@flowspire.com",
                NormalizedEmail = "ADVISOR1@FLOWSPIRE.COM",
                FullName = "Advisor One",
                PasswordHash = hasher.HashPassword(null, "Advisor123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true
            },
            new
            {
                Id = advisor2Id,
                UserName = "advisor2@flowspire.com",
                NormalizedUserName = "ADVISOR2@FLOWSPIRE.COM",
                Email = "advisor2@flowspire.com",
                NormalizedEmail = "ADVISOR2@FLOWSPIRE.COM",
                FullName = "Advisor Two",
                PasswordHash = hasher.HashPassword(null, "Advisor123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true
            },
            new
            {
                Id = customer1Id,
                UserName = "customer1@flowspire.com",
                NormalizedUserName = "CUSTOMER1@FLOWSPIRE.COM",
                Email = "customer1@flowspire.com",
                NormalizedEmail = "CUSTOMER1@FLOWSPIRE.COM",
                FullName = "Customer One",
                PasswordHash = hasher.HashPassword(null, "Customer123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true
            },
            new
            {
                Id = customer2Id,
                UserName = "customer2@flowspire.com",
                NormalizedUserName = "CUSTOMER2@FLOWSPIRE.COM",
                Email = "customer2@flowspire.com",
                NormalizedEmail = "CUSTOMER2@FLOWSPIRE.COM",
                FullName = "Customer Two",
                PasswordHash = hasher.HashPassword(null, "Customer123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true
            },
            new
            {
                Id = customer3Id,
                UserName = "customer3@flowspire.com",
                NormalizedUserName = "CUSTOMER3@FLOWSPIRE.COM",
                Email = "customer3@flowspire.com",
                NormalizedEmail = "CUSTOMER3@FLOWSPIRE.COM",
                FullName = "Customer Three",
                PasswordHash = hasher.HashPassword(null, "Customer123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true
            }
        );

        var adminRoleId = "660e8400-e29b-41d4-a716-446655440001";
        var advisorRoleId = "660e8400-e29b-41d4-a716-446655440002";
        var customerRoleId = "660e8400-e29b-41d4-a716-446655440003";

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = advisorRoleId, Name = "FinancialAdvisor", NormalizedName = "FINANCIALADVISOR" },
            new IdentityRole { Id = customerRoleId, Name = "Customer", NormalizedName = "CUSTOMER" }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminId, RoleId = adminRoleId },
            new IdentityUserRole<string> { UserId = advisor1Id, RoleId = advisorRoleId },
            new IdentityUserRole<string> { UserId = advisor2Id, RoleId = advisorRoleId },
            new IdentityUserRole<string> { UserId = customer1Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customer2Id, RoleId = customerRoleId },
            new IdentityUserRole<string> { UserId = customer3Id, RoleId = customerRoleId }
        );
    }

    private static void SeedCategories(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new { Id = 1, Name = "Alimentação", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 2, Name = "Transporte", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 3, Name = "Saúde", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 4, Name = "Moradia", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 5, Name = "Lazer", UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 6, Name = "Educação", UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 7, Name = "Vestuário", UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 8, Name = "Alimentação", UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 9, Name = "Tecnologia", UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 10, Name = "Lazer", UserId = "550e8400-e29b-41d4-a716-446655440006" }
        );
    }

    private static void SeedTransactions(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        builder.Entity<Transaction>().HasData(
            new { Id = 1, Description = "Supermercado", Amount = -80.00m, Date = now.AddMonths(-2).AddDays(-15), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 2, Description = "Ônibus", Amount = -15.00m, Date = now.AddMonths(-2).AddDays(-10), CategoryId = 2, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 3, Description = "Salário", Amount = 2000.00m, Date = now.AddMonths(-2).AddDays(-1), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },

            new { Id = 4, Description = "Farmácia", Amount = -50.00m, Date = now.AddMonths(-1).AddDays(-5), CategoryId = 3, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 5, Description = "Aluguel", Amount = -800.00m, Date = now.AddMonths(-1).AddDays(-3), CategoryId = 4, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 6, Description = "Supermercado", Amount = -120.00m, Date = now.AddMonths(-1).AddDays(-10), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },

            new { Id = 7, Description = "Supermercado", Amount = -60.00m, Date = now.AddDays(-15), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 8, Description = "Uber", Amount = -25.00m, Date = now.AddDays(-10), CategoryId = 2, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 9, Description = "Salário", Amount = 2200.00m, Date = now.AddDays(-1), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },

            new { Id = 10, Description = "Cinema", Amount = -40.00m, Date = now.AddMonths(-2).AddDays(-7), CategoryId = 5, UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 11, Description = "Curso Online", Amount = -150.00m, Date = now.AddMonths(-2).AddDays(-3), CategoryId = 6, UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 12, Description = "Freelance", Amount = 1000.00m, Date = now.AddMonths(-2).AddDays(-2), CategoryId = 6, UserId = "550e8400-e29b-41d4-a716-446655440005" },

            new { Id = 13, Description = "Roupas", Amount = -90.00m, Date = now.AddMonths(-1).AddDays(-5), CategoryId = 7, UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 14, Description = "Cinema", Amount = -35.00m, Date = now.AddMonths(-1).AddDays(-2), CategoryId = 5, UserId = "550e8400-e29b-41d4-a716-446655440005" },

            new { Id = 15, Description = "Show", Amount = -60.00m, Date = now.AddDays(-8), CategoryId = 5, UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 16, Description = "Freelance", Amount = 1200.00m, Date = now.AddDays(-1), CategoryId = 6, UserId = "550e8400-e29b-41d4-a716-446655440005" },

            new { Id = 17, Description = "Restaurante", Amount = -70.00m, Date = now.AddMonths(-2).AddDays(-12), CategoryId = 8, UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 18, Description = "Notebook", Amount = -1500.00m, Date = now.AddMonths(-2).AddDays(-5), CategoryId = 9, UserId = "550e8400-e29b-41d4-a716-446655440006" },

            new { Id = 19, Description = "Jogo Online", Amount = -50.00m, Date = now.AddMonths(-1).AddDays(-10), CategoryId = 10, UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 20, Description = "Supermercado", Amount = -100.00m, Date = now.AddMonths(-1).AddDays(-3), CategoryId = 8, UserId = "550e8400-e29b-41d4-a716-446655440006" },

            new { Id = 21, Description = "Cinema", Amount = -30.00m, Date = now.AddDays(-7), CategoryId = 10, UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 22, Description = "Salário", Amount = 1800.00m, Date = now.AddDays(-2), CategoryId = 8, UserId = "550e8400-e29b-41d4-a716-446655440006" }
        );
    }

    private static void SeedBudgets(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        builder.Entity<Budget>().HasData(
            new { Id = 1, CategoryId = 1, Amount = 300.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 2, CategoryId = 3, Amount = 200.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 3, CategoryId = 4, Amount = 1000.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440004" },

            new { Id = 4, CategoryId = 5, Amount = 150.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 5, CategoryId = 6, Amount = 400.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440005" },

            new { Id = 6, CategoryId = 8, Amount = 250.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 7, CategoryId = 10, Amount = 100.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440006" }
        );
    }

    private static void SeedMessages(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        builder.Entity<Message>().HasData(
            new { Id = 1, SenderId = "550e8400-e29b-41d4-a716-446655440004", ReceiverId = "550e8400-e29b-41d4-a716-446655440002", Content = "Olá, preciso de ajuda com meu orçamento!", SentAt = now.AddDays(-10), IsRead = false },
            new { Id = 2, SenderId = "550e8400-e29b-41d4-a716-446655440002", ReceiverId = "550e8400-e29b-41d4-a716-446655440004", Content = "Claro, vamos analisar suas despesas.", SentAt = now.AddDays(-9), IsRead = true },
            new { Id = 3, SenderId = "550e8400-e29b-41d4-a716-446655440005", ReceiverId = "550e8400-e29b-41d4-a716-446655440002", Content = "Qual é o melhor investimento agora?", SentAt = now.AddDays(-5), IsRead = false },
            new { Id = 4, SenderId = "550e8400-e29b-41d4-a716-446655440002", ReceiverId = "550e8400-e29b-41d4-a716-446655440005", Content = "Recomendo fundos de índice.", SentAt = now.AddDays(-4), IsRead = true },
            new { Id = 5, SenderId = "550e8400-e29b-41d4-a716-446655440006", ReceiverId = "550e8400-e29b-41d4-a716-446655440003", Content = "Como reduzir meus gastos?", SentAt = now.AddDays(-3), IsRead = false },
            new { Id = 6, SenderId = "550e8400-e29b-41d4-a716-446655440003", ReceiverId = "550e8400-e29b-41d4-a716-446655440006", Content = "Vamos revisar suas categorias.", SentAt = now.AddDays(-2), IsRead = false }
        );
    }

    private static void SeedAdvisorCustomer(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        builder.Entity<AdvisorCustomer>().HasData(
            new { AdvisorId = "550e8400-e29b-41d4-a716-446655440002", CustomerId = "550e8400-e29b-41d4-a716-446655440004", AssignedAt = now.AddDays(-20) },
            new { AdvisorId = "550e8400-e29b-41d4-a716-446655440002", CustomerId = "550e8400-e29b-41d4-a716-446655440005", AssignedAt = now.AddDays(-15) },
            new { AdvisorId = "550e8400-e29b-41d4-a716-446655440003", CustomerId = "550e8400-e29b-41d4-a716-446655440006", AssignedAt = now.AddDays(-10) }
        );
    }
}