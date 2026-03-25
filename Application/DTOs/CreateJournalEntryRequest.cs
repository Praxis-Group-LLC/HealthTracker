namespace Application.DTOs;

public sealed record CreateJournalEntryRequest(
    DateOnly Date,
    string Text,
    int MoodRating,
    int SleepHours,
    string? Tags);