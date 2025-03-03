using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Flowspire.Infra.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
{
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Description).IsRequired().HasMaxLength(200);
            entity.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(t => t.Date).IsRequired();
            entity.Property(t => t.Category).IsRequired().HasMaxLength(50);
            entity.HasOne(t => t.User)
                  .WithMany(u => u.Transactions)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<User>(entity =>
        {
            entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
        });
    }
}