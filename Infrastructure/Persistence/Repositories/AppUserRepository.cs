using Domain.Shared;
using Domain.Users;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class AppUserRepository(HealthTrackerDbContext db) : IAppUserRepository
{
    public async Task<AppUser?> GetByUserIdAsync(
        UserId userId,
        CancellationToken ct = default)
    {
        return await db.AppUsers
            .SingleOrDefaultAsync(p => p.UserId == userId, ct);
    }

    public async Task AddAsync(AppUser user, CancellationToken ct = default)
    {
        await db.AppUsers.AddAsync(user, ct);
    }

    public Task UpdateAsync(AppUser user, CancellationToken ct = default)
    {
        db.AppUsers.Update(user);
        return Task.CompletedTask;
    }

    public async Task UpsertAsync(AppUser profile, CancellationToken ct = default)
    {
        var existing = await GetByUserIdAsync(profile.UserId, ct);
        if (existing is null)
        {
            await AddAsync(profile, ct);
        }
        else
        {
            db.Entry(existing).CurrentValues.SetValues(profile);
        }
    }
}