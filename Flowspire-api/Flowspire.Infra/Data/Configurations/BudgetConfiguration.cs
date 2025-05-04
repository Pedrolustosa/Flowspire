using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasKey(b => b.Id);

        // Map Money Value Object for Amount
        builder.OwnsOne(b => b.Amount, m =>
        {
            m.Property(x => x.Value)
             .HasColumnName("Amount")
             .HasColumnType("decimal(18,2)")
             .IsRequired();
        });

        builder.Property(b => b.StartDate)
               .IsRequired();

        builder.Property(b => b.EndDate)
               .IsRequired();

        // Relationship with Category (no navigation collection in Category)
        builder.HasOne(b => b.Category)
               .WithMany()
               .HasForeignKey(b => b.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        // Relationship with User (no navigation collection in User)
        builder.HasOne(b => b.User)
               .WithMany()
               .HasForeignKey(b => b.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
