using Application.Abstractions;
using Application.DTOs;
using Domain.Shared;

namespace Application.Journaling.Queries;

public sealed record GetJournalEntriesQuery(
    UserId UserId,
    DateOnly From,
    DateOnly To
    ) : IQuery<List<JournalEntryDto>>;