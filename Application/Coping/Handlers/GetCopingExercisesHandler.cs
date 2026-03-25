using Application.Abstractions;
using Application.DTOs;

namespace Application.Coping.Handlers;

public sealed class GetCopingExercisesHandler(ICopingExerciseRepository exercises)
{
    public async Task<List<CopingExerciseDto>> Handle(
        GetCopingExercisesQuery query,
        CancellationToken ct = default)
    {
        var list = await exercises.GetAllAsync(ct);

        return list.Select(CopingExerciseDto.FromDomain).ToList();
    }
}