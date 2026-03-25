using Domain.Shared;

namespace Domain.Journaling;

public sealed class JournalEntry
{
    public JournalEntryId Id { get; init; }        // or a JournalEntryId VO
    public UserId UserId { get; init; }
    public DateOnly Date { get; init; }
    public int MoodRating { get; init; }        // 1–10
    public int SleepHours { get; init; }        // optional
    public string? Tags { get; init; }           // simple string for now
    public string Text { get; init; } = null!;
    
    // ReSharper disable once UnusedMember.Local
    private JournalEntry() { } // EF

    private JournalEntry(UserId userId, DateOnly date, string text,
        int moodRating, int sleepHours, string? tags)
    {
        Id = JournalEntryId.New();
        UserId = userId;
        Date = date;
        Text = text;
        MoodRating = moodRating;
        SleepHours = sleepHours;
        Tags = tags;
    }

    public static JournalEntry Create(
        UserId userId,
        DateOnly date,
        string text,
        int moodRating,
        int sleepHours,
        string? tags = null)
    {
        return string.IsNullOrWhiteSpace(text) ? 
            throw new ArgumentException("Journal text cannot be empty.", nameof(text))
            :
            // you can enforce max length, valid ranges, etc. here
            new JournalEntry(userId, date, text.Trim(), moodRating, sleepHours, tags);
    }
}