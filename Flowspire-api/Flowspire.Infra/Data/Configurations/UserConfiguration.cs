using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure personal data
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.BirthDate)
                   .IsRequired(false);
            builder.Property(u => u.Gender)
                   .IsRequired();

            // Configure address fields (optional)
            builder.Property(u => u.AddressLine1)
                   .HasMaxLength(200);
            builder.Property(u => u.AddressLine2)
                   .HasMaxLength(200);
            builder.Property(u => u.City)
                   .HasMaxLength(100);
            builder.Property(u => u.State)
                   .HasMaxLength(100);
            builder.Property(u => u.Country)
                   .HasMaxLength(100);
            builder.Property(u => u.PostalCode)
                   .HasMaxLength(20);
        }
    }
}
