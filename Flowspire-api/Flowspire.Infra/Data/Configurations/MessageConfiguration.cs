using Flowspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flowspire.Infra.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.SenderId)
                .IsRequired()
                .HasMaxLength(450);
        builder.Property(m => m.ReceiverId)
                .IsRequired()
                .HasMaxLength(450);
        builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(1000);
        builder.Property(m => m.SentAt)
                .IsRequired();
        builder.Property(m => m.IsRead)
                .IsRequired();
        builder.Property(m => m.ReadAt)
                .IsRequired(false);

        builder.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
