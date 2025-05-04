using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        // FullName value object
        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .IsRequired()
                .HasMaxLength(100);
            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(100);
        });

        // PhoneNumber value object
        builder.OwnsOne(u => u.Phone, phone =>
        {
            phone.Property(p => p.Value)
                 .HasColumnName("PhoneNumber")
                 .IsRequired()
                 .HasMaxLength(16);
        });

        // Address value object
        builder.OwnsOne(u => u.Address, addr =>
        {
            addr.Property(a => a.Line1).HasColumnName("AddressLine1").HasMaxLength(200);
            addr.Property(a => a.Line2).HasColumnName("AddressLine2").HasMaxLength(200);
            addr.Property(a => a.City).HasColumnName("City").HasMaxLength(100);
            addr.Property(a => a.State).HasColumnName("State").HasMaxLength(100);
            addr.Property(a => a.Country).HasColumnName("Country").HasMaxLength(100);
            addr.Property(a => a.PostalCode).HasColumnName("PostalCode").HasMaxLength(20);
        });

        // Other scalar props
        builder.Property(u => u.BirthDate).IsRequired(false);
        builder.Property(u => u.Gender).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired();
    }
}