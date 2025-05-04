using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations;

public class AdvisorCustomerConfiguration : IEntityTypeConfiguration<AdvisorCustomer>
{
    public void Configure(EntityTypeBuilder<AdvisorCustomer> builder)
    {
        builder.HasKey(ac => ac.Id);
        builder.Property(ac => ac.AdvisorId).IsRequired().HasMaxLength(450);
        builder.Property(ac => ac.CustomerId).IsRequired().HasMaxLength(450);
        builder.Property(ac => ac.AssignedAt).IsRequired();

        builder.HasOne(ac => ac.Advisor)
               .WithMany()
               .HasForeignKey(ac => ac.AdvisorId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(ac => ac.Customer)
               .WithMany()
               .HasForeignKey(ac => ac.CustomerId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
