using Domain.Coping;

namespace Application.DTOs;

public sealed record CopingExerciseDto(
    CopingExerciseId Id,
    string Code,
    string Name,
    string Description)
{
    public static CopingExerciseDto FromDomain(CopingExercise ex)
    {
        return new CopingExerciseDto(ex.Id, ex.Code, ex.Name, ex.Description);
    }
}