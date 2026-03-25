using Application.Abstractions;
using Application.Journaling.Commands;
using Domain.Journaling;
using MediatR;

namespace Application.Journaling.Handlers;

public sealed class CreateJournalEntryHandler(IJournalEntryRepository journalEntries) : IRequestHandler<CreateJournalEntryCommand, JournalEntryId>
{
    public async Task<JournalEntryId> Handle(CreateJournalEntryCommand command, CancellationToken ct = default)
    {
        var entry = JournalEntry.Create(
            command.UserId,
            command.Date,
            command.Text,
            command.MoodRating,
            command.SleepHours);

        await journalEntries.AddAsync(entry, ct);

        return entry.Id;
    }
}