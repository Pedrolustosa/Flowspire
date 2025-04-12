using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations
{
    public class AdvisorCustomerConfiguration : IEntityTypeConfiguration<AdvisorCustomer>
    {
        public void Configure(EntityTypeBuilder<AdvisorCustomer> builder)
        {
            builder.HasKey(ac => new { ac.AdvisorId, ac.CustomerId });

            builder.HasOne(ac => ac.Advisor)
                   .WithMany()
                   .HasForeignKey(ac => ac.AdvisorId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ac => ac.Customer)
                   .WithMany()
                   .HasForeignKey(ac => ac.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
