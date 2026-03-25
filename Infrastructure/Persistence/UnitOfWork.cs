using Application.Abstractions;

namespace Infrastructure.Persistence;

public sealed class UnitOfWork(HealthTrackerDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => dbContext.SaveChangesAsync(ct);
}