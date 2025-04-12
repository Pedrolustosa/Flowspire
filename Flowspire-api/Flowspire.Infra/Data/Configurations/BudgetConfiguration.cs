using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(b => b.StartDate)
                   .IsRequired();
            builder.Property(b => b.EndDate)
                   .IsRequired();

            builder.HasOne(b => b.Category)
                   .WithMany()
                   .HasForeignKey(b => b.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.User)
                   .WithMany()
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
