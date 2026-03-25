using Domain.Journaling;
using Domain.Shared;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class JournalEntryRepository(HealthTrackerDbContext db) : IJournalEntryRepository
{
    public async Task<JournalEntry?> GetByUserAndDateAsync(
        UserId userId,
        DateOnly date,
        CancellationToken ct = default)
    {
        return await db.JournalEntries
            .Where(e => e.UserId == userId && e.Date == date)
            .SingleOrDefaultAsync(ct);
    }

    public async Task<List<JournalEntry>> GetRangeAsync(
        UserId userId,
        DateOnly from,
        DateOnly to,
        CancellationToken ct = default)
    {
        return await db.JournalEntries
            .Where(e => e.UserId == userId && e.Date >= from && e.Date <= to)
            .OrderBy(e => e.Date)
            .ToListAsync(ct);
    }

    public async Task AddAsync(JournalEntry entry, CancellationToken ct = default)
    {
        await db.JournalEntries.AddAsync(entry, ct);
    }
}