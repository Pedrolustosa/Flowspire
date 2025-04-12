using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
            throw new Exception("Error applying seed data to the database.", ex);
        }
    }

    private static void SeedUsersAndRoles(ModelBuilder builder, PasswordHasher<User> hasher)
    {
        // Define fixed GUIDs for seeded users
        var adminId = "550e8400-e29b-41d4-a716-446655440001";
        var advisor1Id = "550e8400-e29b-41d4-a716-446655440002";
        var advisor2Id = "550e8400-e29b-41d4-a716-446655440003";
        var customer1Id = "550e8400-e29b-41d4-a716-446655440004";
        var customer2Id = "550e8400-e29b-41d4-a716-446655440005";
        var customer3Id = "550e8400-e29b-41d4-a716-446655440006";

        // Choose a fixed DateTime (e.g., January 1, 2023 in UTC)
        var seedDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed Users with all required properties
        builder.Entity<User>().HasData(
            new
            {
                Id = adminId,
                UserName = "admin@flowspire.com",
                NormalizedUserName = "ADMIN@FLOWSPIRE.COM",
                Email = "admin@flowspire.com",
                NormalizedEmail = "ADMIN@FLOWSPIRE.COM",
                FirstName = "Admin",
                LastName = "User",
                BirthDate = (DateTime?)new DateTime(1980, 1, 1),
                Gender = Gender.NotSpecified,
                AddressLine1 = "123 Admin St.",
                AddressLine2 = (string?)null,
                City = "Admin City",
                State = "Admin State",
                Country = "Admin Country",
                PostalCode = "00000",
                PhoneNumber = "+15555550001",
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = advisor1Id,
                UserName = "advisor1@flowspire.com",
                NormalizedUserName = "ADVISOR1@FLOWSPIRE.COM",
                Email = "advisor1@flowspire.com",
                NormalizedEmail = "ADVISOR1@FLOWSPIRE.COM",
                FirstName = "Advisor",
                LastName = "One",
                BirthDate = (DateTime?)new DateTime(1985, 5, 15),
                Gender = Gender.Male,
                AddressLine1 = "101 Advisor Ave.",
                AddressLine2 = (string?)null,
                City = "Advisor City",
                State = "Advisor State",
                Country = "Advisor Country",
                PostalCode = "11111",
                PhoneNumber = "+15555550002",
                PasswordHash = hasher.HashPassword(null, "Advisor123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = advisor2Id,
                UserName = "advisor2@flowspire.com",
                NormalizedUserName = "ADVISOR2@FLOWSPIRE.COM",
                Email = "advisor2@flowspire.com",
                NormalizedEmail = "ADVISOR2@FLOWSPIRE.COM",
                FirstName = "Advisor",
                LastName = "Two",
                BirthDate = (DateTime?)new DateTime(1987, 7, 20),
                Gender = Gender.Male,
                AddressLine1 = "102 Advisor Ave.",
                AddressLine2 = (string?)null,
                City = "Advisor City",
                State = "Advisor State",
                Country = "Advisor Country",
                PostalCode = "11112",
                PhoneNumber = "+15555550003",
                PasswordHash = hasher.HashPassword(null, "Advisor123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = customer1Id,
                UserName = "customer1@flowspire.com",
                NormalizedUserName = "CUSTOMER1@FLOWSPIRE.COM",
                Email = "customer1@flowspire.com",
                NormalizedEmail = "CUSTOMER1@FLOWSPIRE.COM",
                FirstName = "Customer",
                LastName = "One",
                BirthDate = (DateTime?)new DateTime(1990, 3, 10),
                Gender = Gender.Female,
                AddressLine1 = "201 Customer Rd.",
                AddressLine2 = (string?)null,
                City = "Customer City",
                State = "Customer State",
                Country = "Customer Country",
                PostalCode = "22222",
                PhoneNumber = "+15555550004",
                PasswordHash = hasher.HashPassword(null, "Customer123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = customer2Id,
                UserName = "customer2@flowspire.com",
                NormalizedUserName = "CUSTOMER2@FLOWSPIRE.COM",
                Email = "customer2@flowspire.com",
                NormalizedEmail = "CUSTOMER2@FLOWSPIRE.COM",
                FirstName = "Customer",
                LastName = "Two",
                BirthDate = (DateTime?)new DateTime(1992, 8, 25),
                Gender = Gender.Female,
                AddressLine1 = "202 Customer Rd.",
                AddressLine2 = (string?)null,
                City = "Customer City",
                State = "Customer State",
                Country = "Customer Country",
                PostalCode = "22223",
                PhoneNumber = "+15555550005",
                PasswordHash = hasher.HashPassword(null, "Customer123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            },
            new
            {
                Id = customer3Id,
                UserName = "customer3@flowspire.com",
                NormalizedUserName = "CUSTOMER3@FLOWSPIRE.COM",
                Email = "customer3@flowspire.com",
                NormalizedEmail = "CUSTOMER3@FLOWSPIRE.COM",
                FirstName = "Customer",
                LastName = "Three",
                BirthDate = (DateTime?)new DateTime(1995, 12, 5),
                Gender = Gender.NotSpecified,
                AddressLine1 = "203 Customer Rd.",
                AddressLine2 = (string?)null,
                City = "Customer City",
                State = "Customer State",
                Country = "Customer Country",
                PostalCode = "22224",
                PhoneNumber = "+15555550006",
                PasswordHash = hasher.HashPassword(null, "Customer123"),
                SecurityStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );

        // Define Role IDs and seed them along with the user roles
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
            new { Id = 1, Name = "Food", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 2, Name = "Transport", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 3, Name = "Health", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 4, Name = "Housing", UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 5, Name = "Leisure", UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 6, Name = "Education", UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 7, Name = "Clothing", UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 8, Name = "Food", UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 9, Name = "Technology", UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 10, Name = "Leisure", UserId = "550e8400-e29b-41d4-a716-446655440006" }
        );
    }

    private static void SeedTransactions(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        var transactions = new List<object>
        {
            new { Id = 1, Description = "Supermarket", Amount = -80.00m, Date = now.AddMonths(-6).AddDays(-15), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 2, Description = "Bus", Amount = -15.00m, Date = now.AddMonths(-6).AddDays(-10), CategoryId = 2, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 3, Description = "Salary", Amount = 2000.00m, Date = now.AddMonths(-6).AddDays(-1), CategoryId = 1, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 4, Description = "Pharmacy", Amount = -50.00m, Date = now.AddMonths(-5).AddDays(-5), CategoryId = 3, UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 5, Description = "Rent", Amount = -800.00m, Date = now.AddMonths(-5).AddDays(-3), CategoryId = 4, UserId = "550e8400-e29b-41d4-a716-446655440004" },

            new { Id = 20, Description = "Cinema", Amount = -40.00m, Date = now.AddMonths(-6).AddDays(-7), CategoryId = 5, UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 21, Description = "Online Course", Amount = -150.00m, Date = now.AddMonths(-6).AddDays(-3), CategoryId = 6, UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 22, Description = "Freelance", Amount = 1000.00m, Date = now.AddMonths(-6).AddDays(-2), CategoryId = 6, UserId = "550e8400-e29b-41d4-a716-446655440005" },

            new { Id = 35, Description = "Restaurant", Amount = -70.00m, Date = now.AddMonths(-6).AddDays(-12), CategoryId = 8, UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 36, Description = "Notebook", Amount = -1500.00m, Date = now.AddMonths(-6).AddDays(-5), CategoryId = 9, UserId = "550e8400-e29b-41d4-a716-446655440006" },
        };

        builder.Entity<Transaction>().HasData(transactions);
    }

    private static void SeedBudgets(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        var budgets = new List<object>
        {
            new { Id = 1, CategoryId = 1, Amount = 400.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 2, CategoryId = 3, Amount = 250.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440004" },
            new { Id = 3, CategoryId = 4, Amount = 1000.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440004" },

            new { Id = 4, CategoryId = 5, Amount = 200.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 5, CategoryId = 6, Amount = 500.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440005" },
            new { Id = 6, CategoryId = 7, Amount = 300.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(2), UserId = "550e8400-e29b-41d4-a716-446655440005" },

            new { Id = 7, CategoryId = 8, Amount = 300.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 8, CategoryId = 9, Amount = 2000.00m, StartDate = now.AddMonths(-6), EndDate = now.AddMonths(1), UserId = "550e8400-e29b-41d4-a716-446655440006" },
            new { Id = 9, CategoryId = 10, Amount = 150.00m, StartDate = now.AddMonths(-3), EndDate = now.AddMonths(2), UserId = "550e8400-e29b-41d4-a716-446655440006" }
        };

        builder.Entity<Budget>().HasData(budgets);
    }

    private static void SeedMessages(ModelBuilder builder)
    {
        var now = DateTime.UtcNow;
        builder.Entity<Message>().HasData(
            new { Id = 1, SenderId = "550e8400-e29b-41d4-a716-446655440004", ReceiverId = "550e8400-e29b-41d4-a716-446655440002", Content = "Hello, I need help with my budget!", SentAt = now.AddDays(-10), IsRead = false, ReadAt = (DateTime?)null },
            new { Id = 2, SenderId = "550e8400-e29b-41d4-a716-446655440002", ReceiverId = "550e8400-e29b-41d4-a716-446655440004", Content = "Sure, let’s review your expenses.", SentAt = now.AddDays(-9), IsRead = true, ReadAt = now.AddDays(-8) },
            new { Id = 3, SenderId = "550e8400-e29b-41d4-a716-446655440005", ReceiverId = "550e8400-e29b-41d4-a716-446655440002", Content = "What is the best investment now?", SentAt = now.AddDays(-5), IsRead = false, ReadAt = (DateTime?)null },
            new { Id = 4, SenderId = "550e8400-e29b-41d4-a716-446655440002", ReceiverId = "550e8400-e29b-41d4-a716-446655440005", Content = "I recommend index funds.", SentAt = now.AddDays(-4), IsRead = true, ReadAt = now.AddDays(-3) },
            new { Id = 5, SenderId = "550e8400-e29b-41d4-a716-446655440006", ReceiverId = "550e8400-e29b-41d4-a716-446655440003", Content = "How can I reduce my expenses?", SentAt = now.AddDays(-3), IsRead = false, ReadAt = (DateTime?)null },
            new { Id = 6, SenderId = "550e8400-e29b-41d4-a716-446655440003", ReceiverId = "550e8400-e29b-41d4-a716-446655440006", Content = "Let's review your categories.", SentAt = now.AddDays(-2), IsRead = false, ReadAt = (DateTime?)null },
            new { Id = 7, SenderId = "550e8400-e29b-41d4-a716-446655440004", ReceiverId = "550e8400-e29b-41d4-a716-446655440002", Content = "Thank you for your help!", SentAt = now.AddDays(-1), IsRead = false, ReadAt = (DateTime?)null }
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