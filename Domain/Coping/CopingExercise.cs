using Domain.Journaling;
using Domain.Shared;

namespace Domain.Coping;

public sealed class CopingExercise
{
    public CopingExerciseId Id { get; init; }
    public string Code { get; init; } = null!;       // "BOX_BREATHING", "GROUNDING_5_4_3_2_1"
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;

    // Optionally store structured steps as JSON

    // ReSharper disable once UnusedMember.Local
    private CopingExercise() { }

    private CopingExercise(string code, string name, string description)
    {
        Id = CopingExerciseId.New();
        Code = code;
        Name = name;
        Description = description;
    }
    
    public static CopingExercise Create(
        string code,
        string name, 
        string description)
    {
        return new CopingExercise(code, name.Trim(), description.Trim());
    }
}