using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Description)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(t => t.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(t => t.Date)
                   .IsRequired();

            builder.HasOne(t => t.User)
                   .WithMany(u => u.Transactions)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Category)
                   .WithMany(c => c.Transactions)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
