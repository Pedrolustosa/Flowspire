using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        // Nome e descrição
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Description)
               .HasMaxLength(500);

        // Flags e ordenação
        builder.Property(c => c.IsDefault)
               .IsRequired();

        builder.Property(c => c.SortOrder)
               .IsRequired();

        // Timestamps
        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.UpdatedAt)
               .IsRequired();

        // Relacionamento com User
        builder.HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
