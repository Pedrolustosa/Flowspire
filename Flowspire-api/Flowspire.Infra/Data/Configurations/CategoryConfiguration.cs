using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
