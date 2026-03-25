using Domain.Coping;
using Domain.Shared;

namespace Application.Abstractions;

public interface ICopingSessionRepository
{
    Task AddAsync(CopingSession session, CancellationToken ct = default);

    Task<CopingSession?> GetByIdAsync(CopingSessionId id, CancellationToken ct = default);

    Task<List<CopingSession>> GetByUserAsync(
        UserId userId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken ct = default);
}