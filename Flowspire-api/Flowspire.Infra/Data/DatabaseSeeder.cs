using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Infra.Data;

public static class DatabaseSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        var hasher = new PasswordHasher<User>();

        SeedUsersAndRoles(builder, hasher);
        SeedCategories(builder);
        SeedTransactions(builder);
        SeedBudgets(builder);
        SeedBudgetAmounts(builder);
        SeedMessages(builder);
        SeedAdvisorCustomer(builder);
    }

    private static void SeedUsersAndRoles(ModelBuilder builder, PasswordHasher<User> hasher)
    {
        var adminId = "550e8400-e29b-41d4-a716-446655440001";
        var userId = "550e8400-e29b-41d4-a716-446655440002";
        var adminRoleId = "660e8400-e29b-41d4-a716-446655440001";
        var userRoleId = "660e8400-e29b-41d4-a716-446655440002";
        var seedDate = new DateTime(2023, 1, 1);

        // 1) base AspNetUsers
        builder.Entity<User>().HasData(
            new
            {
                Id = adminId,
                UserName = "admin@flowspire.com",
                NormalizedUserName = "ADMIN@FLOWSPIRE.COM",
                Email = "admin@flowspire.com",
                NormalizedEmail = "ADMIN@FLOWSPIRE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "+15555550001",
                PhoneNumberConfirmed = true,
                AccessFailedCount = 0,
                LockoutEnabled = false,
                LockoutEnd = (DateTimeOffset?)null,
                TwoFactorEnabled = false,
                BirthDate = (DateTime?)new DateTime(1980, 1, 1),
                Gender = Gender.NotSpecified,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = userId,
                UserName = "user@flowspire.com",
                NormalizedUserName = "USER@FLOWSPIRE.COM",
                Email = "user@flowspire.com",
                NormalizedEmail = "USER@FLOWSPIRE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "User123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "+15555550002",
                PhoneNumberConfirmed = true,
                AccessFailedCount = 0,
                LockoutEnabled = false,
                LockoutEnd = (DateTimeOffset?)null,
                TwoFactorEnabled = false,
                BirthDate = (DateTime?)new DateTime(1990, 5, 5),
                Gender = Gender.NotSpecified,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );

        // 2) FullName VO
        builder.Entity<User>().OwnsOne(u => u.Name).HasData(
            new { UserId = adminId, FirstName = "Admin", LastName = "User" },
            new { UserId = userId, FirstName = "Test", LastName = "User" }
        );

        // 3) PhoneNumber VO
        builder.Entity<User>().OwnsOne(u => u.Phone).HasData(
            new { UserId = adminId, Value = "+15555550001" },
            new { UserId = userId, Value = "+15555550002" }
        );

        // 4) Address VO
        builder.Entity<User>().OwnsOne(u => u.Address).HasData(
            new
            {
                UserId = adminId,
                Line1 = "123 Admin St.",
                Line2 = (string?)null,
                City = "Admin City",
                State = "Admin State",
                Country = "Admin Country",
                PostalCode = "00000"
            },
            new
            {
                UserId = userId,
                Line1 = "456 Test Ave.",
                Line2 = (string?)null,
                City = "Test City",
                State = (string?)null,
                Country = (string?)null,
                PostalCode = "11111"
            }
        );

        // 5) Roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = userRoleId, Name = "Customer", NormalizedName = "CUSTOMER" }
        );

        // 6) User↔Role
        builder.Entity<IdentityUserRole<string>>().HasData(
            new { UserId = adminId, RoleId = adminRoleId },
            new { UserId = userId, RoleId = userRoleId }
        );
    }

    private static void SeedCategories(ModelBuilder builder)
    {
        var id = 1;
        var now = DateTime.UtcNow;

        builder.Entity<Category>().HasData(
            new { Id = id, Name = "Food", Description = "Food expenses", UserId = "550e8400-e29b-41d4-a716-446655440002", IsDefault = true, SortOrder = 1, CreatedAt = now, UpdatedAt = now },
            new { Id = id+1, Name = "Salary", Description = "Income category", UserId = "550e8400-e29b-41d4-a716-446655440002", IsDefault = true, SortOrder = 2, CreatedAt = now, UpdatedAt = now }
        );
    }

    private static void SeedTransactions(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;

        builder.Entity<FinancialTransaction>().HasData(
            new
            {
                Id = 1,
                Description = "Grocery",
                Amount = -100.00m,
                Fee = 0.00m,
                Discount = 0.00m,
                Date = now,
                Type = TransactionType.Expense,
                CategoryId = 1,
                UserId = "550e8400-e29b-41d4-a716-446655440002",
                Notes = (string?)null,
                PaymentMethod = "Cash",
                IsRecurring = false,
                NextOccurrence = (DateTime?)null,
                CreatedAt = now,
                UpdatedAt = now
            },
            new
            {
                Id = 2,
                Description = "Monthly Salary",
                Amount = 2000.00m,
                Fee = 0.00m,
                Discount = 0.00m,
                Date = now,
                Type = TransactionType.Income,
                CategoryId = 2,
                UserId = "550e8400-e29b-41d4-a716-446655440002",
                Notes = (string?)null,
                PaymentMethod = "Bank",
                IsRecurring = true,
                NextOccurrence = now.AddMonths(1),
                CreatedAt = now,
                UpdatedAt = now
            }
        );
    }

    private static void SeedBudgets(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;

        builder.Entity<Budget>().HasData(
            new { Id = 1, CategoryId = 1, StartDate = now.AddMonths(-1), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440002", CreatedAt = now, UpdatedAt = now }
        );
    }

    private static void SeedBudgetAmounts(ModelBuilder builder)
    {
        builder.Entity<Budget>().OwnsOne(b => b.Amount).HasData(
            new { BudgetId = 1, Value = 500.00m }
        );
    }

    private static void SeedMessages(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;

        builder.Entity<Message>().HasData(
            new { Id = 1, SenderId = "550e8400-e29b-41d4-a716-446655440002", ReceiverId = "550e8400-e29b-41d4-a716-446655440001", Content = "Hello", SentAt = now, IsRead = false, ReadAt = (DateTime?)null }
        );
    }

    private static void SeedAdvisorCustomer(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;

        builder.Entity<AdvisorCustomer>().HasData(
            new { Id = 1, AdvisorId = "550e8400-e29b-41d4-a716-446655440002", CustomerId = "550e8400-e29b-41d4-a716-446655440001", AssignedAt = now }
        );
    }
}