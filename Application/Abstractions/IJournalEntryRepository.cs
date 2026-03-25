using Domain.Journaling;
using Domain.Shared;

namespace Application.Abstractions;

    public interface IJournalEntryRepository
    {
        Task<JournalEntry?> GetByUserAndDateAsync(
            UserId userId,
            DateOnly date,
            CancellationToken ct = default);

        Task<List<JournalEntry>> GetRangeAsync(
            UserId userId,
            DateOnly from,
            DateOnly to,
            CancellationToken ct = default);

        Task AddAsync(JournalEntry entry, CancellationToken ct = default);

        // optional: update/delete methods if you want editing later
    }
