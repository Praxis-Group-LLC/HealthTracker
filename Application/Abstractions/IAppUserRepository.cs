using Domain.Shared;
using Domain.Users;

namespace Application.Abstractions;

public interface IAppUserRepository
{
    Task<AppUser?> GetByUserIdAsync(UserId userId, CancellationToken ct = default);
    Task AddAsync(AppUser user, CancellationToken ct = default);

    Task UpdateAsync(AppUser user, CancellationToken ct = default);

    /// <summary>
    ///     Convenience method if you like “create if not exists”.
    /// </summary>
    Task UpsertAsync(AppUser profile, CancellationToken ct = default);
}