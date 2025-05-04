using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
               .IsRequired()
               .HasMaxLength(128);

        builder.HasIndex(rt => rt.Token)
               .IsUnique();

        builder.Property(rt => rt.UserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.Property(rt => rt.Created)
               .IsRequired();

        builder.Property(rt => rt.Expires)
               .IsRequired();

        builder.Property(rt => rt.IsRevoked)
               .IsRequired();

        builder.HasOne(rt => rt.User)
               .WithMany()
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }

}
