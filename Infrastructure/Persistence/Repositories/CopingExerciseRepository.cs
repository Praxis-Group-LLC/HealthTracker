using Domain.Coping;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CopingExerciseRepository(HealthTrackerDbContext db) : ICopingExerciseRepository
{
    public async Task<List<CopingExercise>> GetAllAsync(CancellationToken ct = default)
    {
        return await db.CopingExercises
            .OrderBy(e => e.Name)
            .ToListAsync(ct);
    }

    public async Task<CopingExercise?> GetByIdAsync(CopingExerciseId id, CancellationToken ct = default)
    {
        return await db.CopingExercises
            .SingleOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<CopingExercise?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await db.CopingExercises
            .SingleOrDefaultAsync(e => e.Code == code, ct);
    }
}