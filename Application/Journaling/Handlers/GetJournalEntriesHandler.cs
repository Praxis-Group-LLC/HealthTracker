using Application.Abstractions;
using Application.DTOs;
using Application.Journaling.Queries;
using MediatR;

namespace Application.Journaling.Handlers;

public sealed class GetJournalEntriesHandler(IJournalEntryRepository journalEntries) : IRequestHandler<GetJournalEntriesQuery, List<JournalEntryDto>>
{
    public async Task<List<JournalEntryDto>> Handle(
        GetJournalEntriesQuery query,
        CancellationToken ct = default)
    {
        var entries = await journalEntries.GetRangeAsync(
            query.UserId,
            query.From,
            query.To,
            ct);

        return entries
            .OrderBy(e => e.Date)
            .Select(JournalEntryDto.FromDomain)
            .ToList();
    }
}