using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data.Configurations;
using Flowspire.Domain.ValueObjects;
using Flowspire.Infra.Data.Converters;

namespace Flowspire.Infra.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
{
    public DbSet<FinancialTransaction> Transactions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<AdvisorCustomer> AdvisorCustomers { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<Money>()
               .HaveConversion<MoneyToDecimalConverter>()
               .HaveColumnType("decimal(18,2)");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new FinancialTransactionConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RefreshTokenConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new BudgetConfiguration());
        builder.ApplyConfiguration(new MessageConfiguration());
        builder.ApplyConfiguration(new AdvisorCustomerConfiguration());
        DatabaseSeeder.Seed(builder);
    }
}