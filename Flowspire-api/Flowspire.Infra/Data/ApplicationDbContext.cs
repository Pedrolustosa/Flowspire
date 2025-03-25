using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Flowspire.Infra.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<AdvisorCustomer> AdvisorCustomers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Description).IsRequired().HasMaxLength(200);
            entity.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(t => t.Date).IsRequired();
            entity.HasOne(t => t.User)
                  .WithMany(u => u.Transactions)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(t => t.Category)
                  .WithMany(c => c.Transactions)
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<User>(entity =>
        {
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        });

        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.Token).IsRequired();
            entity.Property(rt => rt.UserId).IsRequired();
            entity.Property(rt => rt.Created).IsRequired();
            entity.Property(rt => rt.Expires).IsRequired();
            entity.Property(rt => rt.IsRevoked).IsRequired();
            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(rt => rt.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.HasOne(c => c.User)
                  .WithMany()
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Budget>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(b => b.StartDate).IsRequired();
            entity.Property(b => b.EndDate).IsRequired();
            entity.HasOne(b => b.Category)
                  .WithMany()
                  .HasForeignKey(b => b.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(b => b.User)
                  .WithMany()
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Message>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.SenderId).IsRequired();
            entity.Property(m => m.ReceiverId).IsRequired();
            entity.Property(m => m.Content).IsRequired().HasMaxLength(500);
            entity.Property(m => m.SentAt).IsRequired();
            entity.Property(m => m.IsRead).IsRequired();
            entity.Property(m => m.ReadAt).IsRequired(false);
            entity.HasOne(m => m.Sender)
                  .WithMany()
                  .HasForeignKey(m => m.SenderId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(m => m.Receiver)
                  .WithMany()
                  .HasForeignKey(m => m.ReceiverId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AdvisorCustomer>(entity =>
        {
            entity.HasKey(ac => new { ac.AdvisorId, ac.CustomerId });
            entity.HasOne(ac => ac.Advisor)
                  .WithMany()
                  .HasForeignKey(ac => ac.AdvisorId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(ac => ac.Customer)
                  .WithMany()
                  .HasForeignKey(ac => ac.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        DatabaseSeeder.Seed(builder);
    }
}