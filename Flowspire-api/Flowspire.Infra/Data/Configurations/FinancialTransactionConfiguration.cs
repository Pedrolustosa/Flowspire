using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations
{
    public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(t => t.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(t => t.Fee)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired()
                   .HasDefaultValue(0m);

            builder.Property(t => t.Discount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired()
                   .HasDefaultValue(0m);

            builder.Property(t => t.Date)
                   .IsRequired();

            builder.Property(t => t.Type)
                   .IsRequired();

            builder.Property(t => t.Notes)
                   .HasMaxLength(1000);

            builder.Property(t => t.PaymentMethod)
                   .HasMaxLength(100);

            builder.Property(t => t.IsRecurring)
                   .IsRequired();

            builder.Property(t => t.NextOccurrence);

            builder.Property(t => t.CreatedAt)
                   .IsRequired();

            builder.Property(t => t.UpdatedAt)
                   .IsRequired();

            builder.HasOne(t => t.Category)
                   .WithMany(c => c.FinancialTransactions)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
