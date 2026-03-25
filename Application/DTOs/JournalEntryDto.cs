using Domain.Journaling;

namespace Application.DTOs;

public sealed record JournalEntryDto(
    JournalEntryId Id,
    DateOnly Date,
    string Text,
    int? MoodRating,
    int? SleepHours,
    string? Tags)
{
    public static JournalEntryDto FromDomain(JournalEntry domain)
    {
        return new JournalEntryDto(
            domain.Id,
            domain.Date,
            domain.Text,
            domain.MoodRating,
            domain.SleepHours,
            domain.Tags);
    }
}