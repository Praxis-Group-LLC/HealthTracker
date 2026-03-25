using Domain.Coping;

namespace Application.Abstractions;

public interface ICopingExerciseRepository
{
    Task<List<CopingExercise>> GetAllAsync(CancellationToken ct = default);

    Task<CopingExercise?> GetByIdAsync(CopingExerciseId id, CancellationToken ct = default);

    Task<CopingExercise?> GetByCodeAsync(string code, CancellationToken ct = default);
}