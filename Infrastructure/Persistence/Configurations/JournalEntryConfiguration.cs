using Domain.Journaling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        builder.ToTable("journal_entries");

        builder.HasKey(e => e.Id);
        
        builder.Property(c => c.Id)
            .ConfigureJournalEntryId()
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(e => e.UserId)
            .ConfigureUserId()
            .IsRequired();

        builder.Property(e => e.Date)
            .ConfigureDateOnly()
            .IsRequired();

        builder.Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(e => e.MoodRating);
        builder.Property(e => e.SleepHours);

        builder.Property(e => e.Tags)
            .HasMaxLength(500);

        builder.HasIndex(e => new { e.UserId, e.Date })
            .IsUnique(); // one entry per user per day
    }
}