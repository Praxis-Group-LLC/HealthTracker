using Domain.Coping;
using Domain.Shared;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CopingSessionRepository(HealthTrackerDbContext db) : ICopingSessionRepository
{
    public async Task AddAsync(CopingSession session, CancellationToken ct = default)
    {
        await db.CopingSessions.AddAsync(session, ct);
    }

    public async Task<CopingSession?> GetByIdAsync(CopingSessionId id, CancellationToken ct = default)
    {
        return await db.CopingSessions.SingleOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<List<CopingSession>> GetByUserAsync(
        UserId userId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken ct = default)
    {
        return await db.CopingSessions
            .Where(s => s.UserId == userId && s.StartedAt >= from && s.StartedAt <= to)
            .OrderBy(s => s.StartedAt)
            .ToListAsync(ct);
    }
}