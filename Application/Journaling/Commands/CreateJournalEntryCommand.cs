using Application.Abstractions;
using Domain.Journaling;
using Domain.Shared;

namespace Application.Journaling.Commands;

public sealed record CreateJournalEntryCommand(
    UserId UserId,
    DateOnly Date,
    string Text,
    int MoodRating,
    int SleepHours,
    string? Tags
    ) : ICommand<JournalEntryId>;